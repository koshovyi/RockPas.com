using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RockPaperScissors.Core;
using RockPaperScissors.Models;
using RockPaperScissors.Services;
using RockPaperScissors.Services.Games;
using RockPaperScissors.Services.Games.Enums;
using RockPaperScissors.Services.Games.Models;
using System;
using System.Threading.Tasks;

namespace RockPaperScissors.Controllers
{
	public class GameController : BaseController
	{
		private readonly ILogger<GameController> _logger;

		private readonly Config _config;

		public GameController(ILogger<GameController> logger, IOptions<Config> config)
		{
			this._logger = logger;
			this._config = config.Value;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Index(GameNewVm model, [FromServices] GoogleRecaptchaService recaptcha, [FromServices] IGamesService games)
		{
			model.GoogleRecaptchaApiKey = this._config.GoogleRecaptcha.ApiKey;

			if (ModelState.IsValid)
			{
				//Captcha
				string recaptchaResponse = this.Request.Form["g-recaptcha-response"];
				if (!await recaptcha.ValidateRecaptcha(this._config.GoogleRecaptcha.ApiSecret, recaptchaResponse))
					return ViewAlertMessageDanger(model, "Access denied", "Invalid captcha token.");

				//Create form
				Guid session = Guid.NewGuid();
				Game game = new Game()
				{
					CreatedAt = DateTime.UtcNow,
					HandAuthor = model.Hand,
					ID_Session = session,
					NameAuthor = model.NameAuthor,
					NameOpponent = model.NameOpponent,
					Memo = model.Memo,
					Trophy = model.Trophy,
				};
				Game result = games.Create(game);

				//Create authors cookie
				this.Response.Cookies.Append("GameRPS_" + result.ID.ToString(), session.ToString(), new Microsoft.AspNetCore.Http.CookieOptions()
				{
					Expires = DateTime.Now.AddMonths(1),
				});

				return RedirectAlertInfo("/" + result.ID.ToString(), "Success", "The game has been successfully created.");
			}

			return ViewAlertMessageDanger(model, "No valid data", "Please enter valid data: login, password, captcha.");
		}

		private bool IsAuthor(Guid id, Guid session)
		{
			string cookieName = "GameRPS_" + id.ToString();
			bool isAuthor = this.Request.Cookies.ContainsKey(cookieName) && this.Request.Cookies[cookieName] == session.ToString();
			return isAuthor;
		}

		[HttpGet("{id}")]
		public IActionResult Index(Guid id, [FromServices] IGamesService games)
		{
			if (games.Exist(id))
			{
				Game game = games.Get(id);
				bool isAuthor = IsAuthor(id, game.ID_Session);

				GameViewVm model = new GameViewVm()
				{
					ID = game.ID,
					NameAuthor = game.NameAuthor,
					NameOpponent = game.NameOpponent,
					HandAuthor = game.HandAuthor,
					HandOpponent = game.HandOpponent,
					Result = game.Result,
					Trophy = game.Trophy,
					Memo = game.Memo,
					IsAuthor = isAuthor,
					IsOpponent = !isAuthor,
				};

				return View(model);
			}
			return NotFound();
		}

		[HttpPost("{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Index(Guid id, RPS Hand, [FromServices] IGamesService games)
		{
			if (Hand == RPS.UNKNOWN || id == Guid.Empty)
				return BadRequest();

			if (games.Exist(id))
			{
				Game game = games.Get(id);

				//Game with result
				if (game.Result != Result.UNKNOWN)
					return BadRequest();

				//Author
				bool isAuthor = IsAuthor(id, game.ID_Session);
				if (isAuthor)
					return BadRequest();

				Result result = CheckResult(game.HandAuthor, Hand);
				game.FinishedAt = DateTime.UtcNow;
				game.HandOpponent = Hand;
				game.Result = result;
				games.Update(game);

			}
			return Redirect("/" + id.ToString());
		}

		private Result CheckResult(RPS author, RPS opponent)
		{
			if (author == opponent)
				return Result.DRAW;

			if (author == RPS.ROCK && opponent == RPS.SCISSORS)
				return Result.AUTHOR_WIN;
			if (author == RPS.ROCK && opponent == RPS.PAPER)
				return Result.OPPONENT_WIN;

			if (author == RPS.PAPER && opponent == RPS.ROCK)
				return Result.AUTHOR_WIN;
			if (author == RPS.PAPER && opponent == RPS.SCISSORS)
				return Result.OPPONENT_WIN;

			if (author == RPS.SCISSORS && opponent == RPS.PAPER)
				return Result.AUTHOR_WIN;
			if (author == RPS.SCISSORS && opponent == RPS.ROCK)
				return Result.OPPONENT_WIN;

			return Result.UNKNOWN;
		}

	}
}
