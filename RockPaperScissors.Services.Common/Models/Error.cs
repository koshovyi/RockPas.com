using RockPaperScissors.Database;
using SqlBuilder.Attributes;
using System;

namespace RockPaperScissors.Services.Common.Models
{

	[TableName(Constants.COMMON_ERRORS)]
	public class Error
	{
		
		[IgnoreUpdate]
		[InsertDefault("@uuid")]
		public Guid ID { get; set; }

		[IgnoreUpdate]
		public Guid? ID_Profile { get; set; }

		[IgnoreUpdate]
		public DateTime CreatedAt { get; set; }

		public string Path { get; set; }

		public string Exception { get; set; }

		public string Memo { get; set; }

		public Error()
		{
			this.CreatedAt = DateTime.UtcNow;
		}

	}

}
