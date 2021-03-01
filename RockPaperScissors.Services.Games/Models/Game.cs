using RockPaperScissors.Database;
using RockPaperScissors.Services.Games.Enums;
using SqlBuilder.Attributes;
using System;

namespace RockPaperScissors.Services.Games.Models
{

	[TableName(Constants.TABLE_GAMES)]
	public class Game
	{

		[IgnoreUpdate]
		[InsertDefault("@uuid")]
		public Guid ID { get; set; }

		[IgnoreUpdate]
		public Guid ID_Session { get; set; }

		[IgnoreUpdate]
		public DateTime CreatedAt { get; set; }

		[IgnoreInsert]
		public DateTime? FinishedAt { get; set; }

		[IgnoreUpdate]
		public RPS HandAuthor { get; set; }

		public RPS HandOpponent { get; set; }

		[IgnoreUpdate]
		public string NameAuthor { get; set; }

		[IgnoreUpdate]
		public string NameOpponent { get; set; }

		[IgnoreUpdate]
		public string Trophy { get; set; }

		[IgnoreUpdate]
		public string Memo { get; set; }

		public Result Result { get; set; }

	}

}
