using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RockPaperScissors.Core;
using RockPaperScissors.Models;
using RockPaperScissors.Services;
using RockPaperScissors.Services.Support;
using RockPaperScissors.Services.Support.Models;
using System.Threading.Tasks;

namespace RockPaperScissors.Controllers
{
	public class SupportController : BaseController
	{

		private readonly ILogger<SupportController> _logger;

		private readonly Config _config;

		public SupportController(ILogger<SupportController> logger, IOptions<Config> config)
		{
			_logger = logger;
			_config = config.Value;
		}

		[HttpGet("about")]
		public IActionResult About()
		{
			return View();
		}

		[HttpGet("contacts")]
		public IActionResult Contacts()
		{
			ContactsVw model = new ContactsVw() 
			{ 
				GoogleRecaptchaApiKey = this._config.GoogleRecaptcha.ApiKey,
			};
			return View(model);
		}

		[HttpPost("contacts")]
		public async Task<IActionResult> Contacts(ContactsVw model, [FromServices] GoogleRecaptchaService recaptcha, [FromServices] ISupportContactUsService support)
		{
			model.GoogleRecaptchaApiKey = this._config.GoogleRecaptcha.ApiKey;

			if (ModelState.IsValid)
			{
				//Captcha
				string recaptchaResponse = this.Request.Form["g-recaptcha-response"];
				if (!await recaptcha.ValidateRecaptcha(this._config.GoogleRecaptcha.ApiSecret, recaptchaResponse))
					return ViewAlertMessageDanger(model, "Access denied", "Invalid captcha token.");

				//Create form
				ContactUs form = new ContactUs()
				{
					ID_Profile = GetUserIDOrNull(),
					Name = model.Name,
					Contacts = model.Contacts,
					Memo = model.Memo,
					IP = HttpContext.Connection.RemoteIpAddress.ToString(),
					UserAgent = Request.Headers["User-Agent"],
				};
				ContactUs result = support.Create(form);

				//Clear
				model.Name = string.Empty;
				model.Contacts = string.Empty;
				model.Memo = string.Empty;

				return ViewAlertMessageSuccess(model, "Thank you!", "Your message has been submitted. We will contact you shortly.");
			}

			return ViewAlertMessageDanger(model, "No valid data", "Please enter valid data: login, password, captcha.");
		}

	}
}
