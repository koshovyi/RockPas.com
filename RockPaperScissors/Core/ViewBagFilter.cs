using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using RockPaperScissors.Services.Profiles;
using RockPaperScissors.Services.Profiles.Models;
using System;

namespace RockPaperScissors.Core
{

	public class ViewBagFilter : IResultFilter
	{

		private readonly IProfileService _profiles;

		public ViewBagFilter(IProfileService profiles)
		{
			this._profiles = profiles;
		}

		public void OnResultExecuting(ResultExecutingContext context)
		{
			if (context.Controller is BaseController ctrl && context.HttpContext.Response.StatusCode == StatusCodes.Status200OK)
			{
				ctrl.ViewBag.IsAuth = context.HttpContext.User.Identity.IsAuthenticated;

				if (context.HttpContext.User.Identity.IsAuthenticated)
				{
					Guid guid = ctrl.UserId;
					ctrl.ViewBag.UserId = guid;

					//ViewBag
					Profile profile = _profiles.Get(guid);
					ctrl.ViewBag.UserName = profile.Login;
					ctrl.ViewBag.UserDisplayName = profile.DisplayName;
					ctrl.ViewBag.UserFirstName = profile.FirstName;
					ctrl.ViewBag.UserLastName = profile.LastName;
					ctrl.ViewBag.UserRole = profile.Role;
					//ctrl.ViewBag.UserAvatar = this._iopath.GetProfileUrlOrEmpty(guid);
					//ctrl.ViewBag.UserCover = this._iopath.GetCoverRelative(guid);
					//ctrl.ViewBag.UserCoverExist = this._iopath.CoverExist(guid);

					//Last online update
					_profiles.SetOnline(guid);
				}
			}
		}

		public void OnResultExecuted(ResultExecutedContext context)
		{
		}

	}


}
