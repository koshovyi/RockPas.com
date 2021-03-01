using RockPaperScissors.Services.Games.Enums;
using System;

namespace RockPaperScissors.Models
{

	public class GameViewVm
	{

		public Guid ID { get; set; }

		public bool IsAuthor { get; set; }

		public bool IsOpponent { get; set; }

		public string Memo { get; set; }

		public string Trophy { get; set; }

		public string NameAuthor { get; set; }

		public string NameOpponent { get; set; }

		public RPS HandAuthor { get; set; }

		public RPS HandOpponent { get; set; }

		public Result Result { get; set; }

	}

}
