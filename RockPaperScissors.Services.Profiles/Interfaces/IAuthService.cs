using System;

namespace RockPaperScissors.Services.Profiles
{

	public interface IAuthService
	{

		bool CheckLoginAndPassword(string login, string password);

		Guid GetIdByLoginAndPassword(string login, string password);

	}

}
