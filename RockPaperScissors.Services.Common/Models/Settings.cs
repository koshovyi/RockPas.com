using RockPaperScissors.Database;
using RockPaperScissors.Services.Common.Enums;
using SqlBuilder.Attributes;
using System;

namespace RockPaperScissors.Services.Common.Models
{

	public class Settings
	{
		
		[IgnoreUpdate]
		[InsertDefault("@uuid")]
		public Guid ID { get; set; }

		[IgnoreUpdate]
		public Guid ID_Item { get; set; }

		[IgnoreUpdate]
		public SettingsType Type { get; set; }

		[IgnoreUpdate]
		public string Key { get; set; }

	}

	public interface ISettingType<T>
	{

		T Value { get; set; }

	}

	[TableName(Constants.TABLE_SETTINGS_BOOLS)]
	public class SettingsBool : Settings, ISettingType<bool>
	{

		public bool Value { get; set; }

	}


	[TableName(Constants.TABLE_SETTINGS_INTS)]
	public class SettingsInt : Settings, ISettingType<int>
	{

		public int Value { get; set; }

	}

	[TableName(Constants.TABLE_SETTINGS_STRINGS)]
	public class SettingsString : Settings, ISettingType<string>
	{

		public string Value { get; set; }

	}

}
