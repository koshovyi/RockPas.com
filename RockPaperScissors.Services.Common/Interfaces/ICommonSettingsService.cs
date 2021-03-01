using RockPaperScissors.Services.Common.Models;
using System;

namespace RockPaperScissors.Services.Common
{

	public interface ICommonSettingsService<T, V>
	{

		void ClearAll(Guid id_item);

		T[] GetAll(Guid id_item);

		T Get(Guid id);

		T Get(Guid id_item, string key);

		T Append(Guid id_item, T value);

		void Set(Guid id, V value);

		void Remove(Guid id);

	}

	public interface ICommonSettingsServiceBool : ICommonSettingsService<SettingsBool, bool>
	{
	}

	public interface ICommonSettingsServiceInt : ICommonSettingsService<SettingsInt, int>
	{
	}

	public interface ICommonSettingsServiceString : ICommonSettingsService<SettingsString, string>
	{
	}

}
