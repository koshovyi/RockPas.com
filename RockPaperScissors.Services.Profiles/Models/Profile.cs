using RockPaperScissors.Database;
using RockPaperScissors.Services.Profiles.Enums;
using SqlBuilder.Attributes;
using System;

namespace RockPaperScissors.Services.Profiles.Models
{

	[TableName(Constants.TABLE_PROFILES)]
	public class Profile
	{

		[IgnoreUpdate]
		[InsertDefault("@uuid")]
		public Guid ID { get; set; }

		[ForeignKey]
		[IgnoreInsert]
		public int? ID_Country { get; set; }

		//dates

		[IgnoreUpdate]
		public DateTime CreatedAt { get; set; }

		[IgnoreInsert]
		public DateTime UpdatedAt { get; set; }

		[IgnoreUpdate, IgnoreInsert]
		public DateTime OnlineAt { get; set; }

		[IgnoreInsert]
		public DateTime Birthdate { get; set; }

		//strings

		[IgnoreUpdate]
		public string Login { get; set; }

		public string Password { get; set; }

		public string DisplayName { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public string Phone { get; set; }

		public string Url { get; set; }

		public string Memo { get; set; }

		public string FacebookId { get; set; }

		public string FacebookData { get; set; }

		public string GoogleId { get; set; }

		public string GoogleData { get; set; }

		//enums

		public Sex Sex { get; set; } = Sex.UNKNOWN;

		public RoleType Role { get; set; } = RoleType.UNKNOWN;

		//flags

		[IgnoreInsert]
		public bool IsEmailConfirmed { get; set; }

		[IgnoreInsert]
		public bool IsPhoneConfirmed { get; set; }

		[IgnoreInsert]
		public bool IsVerified { get; set; }

		[IgnoreInsert]
		public bool IsLocked { get; set; }

		[IgnoreInsert]
		public bool IsDeleted { get; set; }

	}

}
