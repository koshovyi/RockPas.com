using System.ComponentModel.DataAnnotations;

namespace RockPaperScissors.Services.Profiles.Enums
{

	public enum Sex : uint
	{
		[Display(Name = "Not set")]
		UNKNOWN = 0,

		[Display(Name = "Male")]
		MALE = 1,

		[Display(Name = "Female")]
		FEMALE = 2,

		[Display(Name = "Other")]
		OTHER = 3,
	}

}
