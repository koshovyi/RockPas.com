using RockPaperScissors.Database;
using RockPaperScissors.Services.Common.Enums;
using SqlBuilder.Attributes;
using System;

namespace RockPaperScissors.Services.Common.Models
{

	[TableName(Constants.COMMON_LOGS)]
	public class Log
	{
		
		[IgnoreUpdate]
		[InsertDefault("@uuid")]
		public Guid ID { get; set; }

		[IgnoreUpdate]
		public Guid? ID_Profile { get; set; }

		[IgnoreUpdate]
		public DateTime CreatedAt { get; set; }

		public LogType Type { get; set; }

		public string Json { get; set; }

		public Log()
		{
			this.CreatedAt = DateTime.UtcNow;
		}

	}

}
