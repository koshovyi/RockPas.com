using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RockPaperScissors.Core;
using RockPaperScissors.Models;
using RockPaperScissors.Services.Games;
using RockPaperScissors.Services.Games.Models;
using System;

namespace RockPaperScissors.Controllers
{
	public class HomeController : Controller
	{

		private readonly ILogger<HomeController> _logger;

		private readonly Config _config;

		public HomeController(ILogger<HomeController> logger, IOptions<Config> options)
		{
			_logger = logger;
			_config = options.Value;
		}

		private Services.Games.Enums.RPS GetRandomHand()
		{
			Random r = new Random(DateTime.Now.Millisecond);
			int v = r.Next(1, 999);
			if (v <= 333)
				return Services.Games.Enums.RPS.ROCK;
			else if (v <= 666)
				return Services.Games.Enums.RPS.PAPER;
			else
				return Services.Games.Enums.RPS.SCISSORS;
		}

		[HttpGet]
		public IActionResult Index([FromServices] IGamesService games)
		{
			GameNewVm model = new GameNewVm()
			{
				Hand = GetRandomHand(),
				GoogleRecaptchaApiKey = _config.GoogleRecaptcha.ApiKey,
			};

			if (!string.IsNullOrEmpty(this.Request.Query["prev"]) && Guid.TryParse(this.Request.Query["prev"], out Guid id) && games.Exist(id))
			{
				bool a = this.Request.Query["a"] == "1";
				Game game = games.Get(id);
				model.NameAuthor = a ? game.NameAuthor : game.NameOpponent;
				model.NameOpponent = a ? game.NameOpponent : game.NameAuthor;
				model.Memo = game.Memo;
				model.Trophy = game.Trophy;
			}
			return View(model);
		}

		[HttpGet("donate")]
		public IActionResult Donate()
		{
			return View();
		}

		[HttpGet("paypal")]
		public IActionResult Paypal()
		{
			return Redirect(_config.Donate.PayPal);
		}

	}
}
