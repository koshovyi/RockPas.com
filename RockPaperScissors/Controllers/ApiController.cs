using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RockPaperScissors.Core;
using RockPaperScissors.Services.Games;
using RockPaperScissors.Services.Games.Models;
using System;

namespace RockPaperScissors.Controllers
{

	[ApiController]
	[Route("api")]
	public class ApiController : BaseController
	{
		private readonly ILogger<ApiController> _logger;

		private readonly Config _config;

		public ApiController(ILogger<ApiController> logger, IOptions<Config> config)
		{
			this._logger = logger;
			this._config = config.Value;
		}

		[HttpGet("check-result/{id}")]
		public IActionResult CheckResult(Guid id, [FromServices] IGamesService games)
		{
			if (games.Exist(id))
			{
				Game game = games.Get(id);

				var data = new
				{
					game.Result,
				};
				return Json(data);
			}

			return NotFound();
		}

	}
}
