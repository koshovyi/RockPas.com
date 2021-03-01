using RockPaperScissors.Services.Support.Models;
using System;

namespace RockPaperScissors.Services.Support
{

	public interface ISupportArticleService
	{

		Article Create(Article article);

		Article Get(Guid id);

		Article Get(string link);

		void Update(Article article);

	}

}
