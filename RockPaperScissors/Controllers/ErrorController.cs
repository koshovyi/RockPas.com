using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RockPaperScissors.Core;
using RockPaperScissors.Services.Common;
using RockPaperScissors.Services.Common.Models;
using System;

namespace RockPaperScissors.Controllers
{

	[Route("{controller}")]
	public class ErrorController : BaseController
	{

		private readonly ILogger<ErrorController> _logger;

		private readonly ICommonErrorService _errors;

		public ErrorController(ILogger<ErrorController> logger, ICommonErrorService errors)
		{
			this._logger = logger;
			this._errors = errors;
		}

		[Route("404")]
		[HttpGet]
		public IActionResult Error404()
		{
			return View();
		}

		[Route("500")]
		[HttpGet]
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error500()
		{
			var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
			if (exception != null && exception.Error != null)
			{
				try
				{
					AppenExceptionToErrors(exception.Error, exception.Path);
				}
				catch
				{
				}
			}
			return View();
		}

		private void AppenExceptionToErrors(Exception ex, string path)
		{
			_errors.Append(new Error()
			{
				ID_Profile = this.GetUserIDOrNull(),
				Exception = ex.GetType().Name,
				Memo = ex.ToString(),
				Path = path,
			});
		}

	}
}
