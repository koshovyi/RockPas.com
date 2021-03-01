using RockPaperScissors.Database;
using SqlBuilder.Attributes;
using System;

namespace RockPaperScissors.Services.Support.Models
{

	[TableName(Constants.TABLE_SUPPORT_CONTACT_US)]
	public class ContactUs
	{

		[IgnoreUpdate]
		[InsertDefault("@uuid")]
		public Guid ID { get; set; }

		public Guid? ID_Profile { get; set; }

		[IgnoreUpdate]
		public DateTime CreatedAt { get; set; }

		[IgnoreInsert, IgnoreUpdate]
		public DateTime? ReadedAt { get; set; }

		public string Name { get; set; }

		public string Contacts { get; set; }

		public string Memo { get; set; }

		public string IP { get; set; }

		public string UserAgent { get; set; }

		[IgnoreInsert]
		public bool IsAnswered { get; set; }

		[IgnoreInsert]
		public bool IsReaded { get; set; }

		[IgnoreInsert]
		public bool IsDeleted { get; set; }

		public ContactUs()
		{
			this.CreatedAt = DateTime.UtcNow;
		}

	}

}
