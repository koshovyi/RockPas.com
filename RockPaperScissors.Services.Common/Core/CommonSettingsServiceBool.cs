using Dapper;
using RockPaperScissors.Database;
using RockPaperScissors.Services.Common.Models;
using SqlBuilder;
using SqlBuilder.Linq;
using System;
using System.Linq;

namespace RockPaperScissors.Services.Common
{

	public class CommonSettingsServiceBool : ICommonSettingsServiceBool
	{

		private readonly Db _db;

		private readonly string SQL_CLEAR_ALL = new Query<SettingsBool>(Format.MySQL).Delete().Where("id_item").GetSql();

		private readonly string SQL_CREATE = Db.GetSQLInsertForDapperGuid(new Query<SettingsBool>(Format.MySQL).Insert());

		private readonly string SQL_GET_BY_ID = new Query<SettingsBool>(Format.MySQL).Select().Where("id").GetSql();

		private readonly string SQL_GET_ALL = new Query<SettingsBool>(Format.MySQL).Select().Where("id_item").GetSql();

		private readonly string SQL_REMOVE = new Query<SettingsBool>(Format.MySQL).Delete().Where("id").GetSql();

		private readonly string SQL_SET = new Query<SettingsBool>(Format.MySQL).Update().Sets("value").Where("id").GetSql();

		public CommonSettingsServiceBool(Db db)
		{
			this._db = db;
		}

		public SettingsBool Append(Guid id_item, SettingsBool value)
		{
			value.ID_Item = id_item;

			using (var db = this._db.Open())
			{
				return db.Query<SettingsBool>(SQL_CREATE, value).First();
			}
		}

		public void ClearAll(Guid id_item)
		{
			using (var db = this._db.Open())
			{
				db.Execute(SQL_CLEAR_ALL, new { id_item });
			}
		}

		public SettingsBool Get(Guid id)
		{
			using (var db = this._db.Open())
			{
				return db.Query<SettingsBool>(SQL_GET_BY_ID, new { id }).First();
			}
		}

		public SettingsBool Get(Guid id_item, string key)
		{
			throw new NotImplementedException();
		}

		public SettingsBool[] GetAll(Guid id_item)
		{
			using (var db = this._db.Open())
			{
				return db.Query<SettingsBool>(SQL_GET_ALL, new { id_item }).ToArray();
			}
		}

		public void Remove(Guid id)
		{
			using (var db = this._db.Open())
			{
				db.Execute(SQL_REMOVE, new { id });
			}
		}

		public void Set(Guid id, bool value)
		{
			using (var db = this._db.Open())
			{
				db.Execute(SQL_SET, new { id, value });
			}
		}

	}

}
