using System;

namespace RockPaperScissors.Services.Profiles
{

	public interface IProfileService
	{

		//crud

		Models.Profile Get(Guid id);

		Models.Profile Get(string login);

		Models.Profile[] Get(Guid[] ids);

		Models.Profile GetByFacebookId(string facebookId);

		Models.Profile GetByGoogleId(string googleId);

		Models.Profile Create(Models.Profile profile);

		//void

		void Update(Models.Profile profile);

		void SetOnline(Guid id);

		void ChangePassword(Guid id, string password);

		void ChangeEmail(Guid id, string email);

		//flags

		void StatusActivate(Guid id, bool status);

		void StatusEnable(Guid id, bool status);

		void StatusConfirm(Guid id, bool status);

		//bools

		bool ExistLogin(string login);

		bool ExistEmail(string email);

		bool CheckFacebookId(string facebookId);

		bool CheckGoogleId(string facebookId);

	}

}
