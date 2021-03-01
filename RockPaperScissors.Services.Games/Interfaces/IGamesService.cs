using RockPaperScissors.Services.Games.Models;
using System;

namespace RockPaperScissors.Services.Games
{

	public interface IGamesService
	{

		Game Create(Game article);

		Game Get(Guid id);

		bool Exist(Guid id);

		void Update(Game game);

	}

}
