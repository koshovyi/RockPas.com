using System.ComponentModel.DataAnnotations;

namespace RockPaperScissors.Models
{

	public class ContactsVw
	{

		[MaxLength(20)]
		[MinLength(2)]
		[Required(ErrorMessage = "This field is required.")]
		public string Name { get; set; }

		[MaxLength(240)]
		[MinLength(5)]
		[Required(ErrorMessage = "This field is required.")]
		public string Contacts { get; set; }

		[MaxLength(4096)]
		[MinLength(16)]
		[Required(ErrorMessage = "This field is required.")]
		public string Memo { get; set; }

		public string GoogleRecaptchaApiKey { get; set; }

	}

}
