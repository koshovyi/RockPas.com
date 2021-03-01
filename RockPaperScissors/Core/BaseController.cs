using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace RockPaperScissors.Core
{

	public class BaseController : Controller
	{

		public BaseController()
		{
		}

		#region Properties

		internal Guid UserId
		{
			get
			{
				string guid = this.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
				if (string.IsNullOrEmpty(guid))
					return Guid.Empty;
				return new Guid(guid);
			}
		}

		internal string UserLogin
		{
			get
			{
				return this.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
			}
		}

		internal string UserDisplayName
		{
			get
			{
				return this.User?.Claims?.FirstOrDefault(c => c.Type == "DisplayName")?.Value;
			}
		}

		internal bool IsAuth
		{
			get
			{
				if (this.User == null)
					return false;
				return this.User.Identity.IsAuthenticated;
			}
		}

		#endregion

		#region Alert messages

		private void AlertMessage(BootstrapAlertType type, string title, string message, bool closeButton = true, bool headerSeparator = false)
		{
			TempData.Add("AlertVisible", true);
			TempData.Add("AlertType", (int)type);
			TempData.Add("AlertTitle", title);
			TempData.Add("AlertMessage", message);
			TempData.Add("CloseButton", closeButton);
			TempData.Add("HeaderSeparator", headerSeparator);
		}

		public void AlertMessageInfo(string title, string message, bool closeButton = true, bool headerSeparator = false) =>
			AlertMessage(BootstrapAlertType.INFO, title, message, closeButton, headerSeparator);
		public void AlertMessageDanger(string title, string message, bool closeButton = true, bool headerSeparator = false) =>
			AlertMessage(BootstrapAlertType.DANGER, title, message, closeButton, headerSeparator);
		public void AlertMessageWarning(string title, string message, bool closeButton = true, bool headerSeparator = false) =>
			AlertMessage(BootstrapAlertType.WARNING, title, message, closeButton, headerSeparator);
		public void AlertMessageSuccess(string title, string message, bool closeButton = true, bool headerSeparator = false) =>
			AlertMessage(BootstrapAlertType.SUCCESS, title, message, closeButton, headerSeparator);

		#endregion

		#region View with Alert, Redirect with Alert

		private IActionResult ViewAlertMessage<T>(string viewName, T model, BootstrapAlertType type, string title, string message, bool closeButton = true, bool headerSeparator = false)
		{
			this.AlertMessage(type, title, message, closeButton, headerSeparator);

			if (string.IsNullOrEmpty(viewName))
				return View(model);
			else
				return View(viewName, model);
		}

		private IActionResult RedirectAlertMessage(string actionName, BootstrapAlertType type, string title, string message, bool closeButton = true, bool headerSeparator = false)
		{
			this.AlertMessage(type, title, message, closeButton, headerSeparator);

			return Redirect(actionName);
		}

		//with viewName
		public IActionResult ViewAlertMessageInfo<T>(string viewName, T model, string title, string message, bool closeButton = true, bool headerSeparator = false) =>
			ViewAlertMessage(viewName, model, BootstrapAlertType.INFO, title, message, closeButton, headerSeparator);
		public IActionResult ViewAlertMessageDanger<T>(string viewName, T model, string title, string message, bool closeButton = true, bool headerSeparator = false) =>
			ViewAlertMessage(viewName, model, BootstrapAlertType.DANGER, title, message, closeButton, headerSeparator);
		public IActionResult ViewAlertMessageWarning<T>(string viewName, T model, string title, string message, bool closeButton = true, bool headerSeparator = false) =>
			ViewAlertMessage(viewName, model, BootstrapAlertType.WARNING, title, message, closeButton, headerSeparator);
		public IActionResult ViewAlertMessageSuccess<T>(string viewName, T model, string title, string message, bool closeButton = true, bool headerSeparator = false) =>
			ViewAlertMessage(viewName, model, BootstrapAlertType.SUCCESS, title, message, closeButton, headerSeparator);

		//without viewName
		public IActionResult ViewAlertMessageInfo<T>(T model, string title, string message, bool closeButton = true, bool headerSeparator = false) =>
			ViewAlertMessage(string.Empty, model, BootstrapAlertType.INFO, title, message, closeButton, headerSeparator);
		public IActionResult ViewAlertMessageDanger<T>(T model, string title, string message, bool closeButton = true, bool headerSeparator = false) =>
			ViewAlertMessage(string.Empty, model, BootstrapAlertType.DANGER, title, message, closeButton, headerSeparator);
		public IActionResult ViewAlertMessageWarning<T>(T model, string title, string message, bool closeButton = true, bool headerSeparator = false) =>
			ViewAlertMessage(string.Empty, model, BootstrapAlertType.WARNING, title, message, closeButton, headerSeparator);
		public IActionResult ViewAlertMessageSuccess<T>(T model, string title, string message, bool closeButton = true, bool headerSeparator = false) =>
			ViewAlertMessage(string.Empty, model, BootstrapAlertType.SUCCESS, title, message, closeButton, headerSeparator);

		//redirect
		public IActionResult RedirectAlertInfo(string url, string title, string message, bool closeButton = true, bool headerSeparator = false) =>
			RedirectAlertMessage(url, BootstrapAlertType.INFO, title, message, closeButton, headerSeparator);
		public IActionResult RedirectAlertDanger(string url, string title, string message, bool closeButton = true, bool headerSeparator = false) =>
			RedirectAlertMessage(url, BootstrapAlertType.DANGER, title, message, closeButton, headerSeparator);
		public IActionResult RedirectAlertWarning(string url, string title, string message, bool closeButton = true, bool headerSeparator = false) =>
			RedirectAlertMessage(url, BootstrapAlertType.WARNING, title, message, closeButton, headerSeparator);
		public IActionResult RedirectAlertSuccess(string url, string title, string message, bool closeButton = true, bool headerSeparator = false) =>
			RedirectAlertMessage(url, BootstrapAlertType.SUCCESS, title, message, closeButton, headerSeparator);

		#endregion

		#region Common

		public Guid? GetUserIDOrNull()
		{
			if (this.User.Identity.IsAuthenticated && this.UserId != Guid.Empty)
				return this.UserId;
			return null;
		}

		#endregion

	}

}
