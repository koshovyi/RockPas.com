using RockPaperScissors.Services.Games.Enums;
using System.ComponentModel.DataAnnotations;

namespace RockPaperScissors.Models
{

	public class GameNewVm
	{

		public string GoogleRecaptchaApiKey { get; set; }

		public string Memo { get; set; }

		[MaxLength(64)]
		public string Trophy { get; set; }

		[Required(ErrorMessage = "The field is required.")]
		[MaxLength(21)]
		public string NameAuthor { get; set; }

		[Required(ErrorMessage = "The field is required.")]
		[MaxLength(21)]
		public string NameOpponent { get; set; }

		public RPS Hand { get; set; }

	}

}
