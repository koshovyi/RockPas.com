using Dapper;
using RockPaperScissors.Database;
using RockPaperScissors.Services.Support.Models;
using SqlBuilder;
using SqlBuilder.Linq;
using System;
using System.Linq;

namespace RockPaperScissors.Services.Support
{

	public class SupportContactUsService : ISupportContactUsService
	{

		private readonly Db _db;

		private readonly string SQL_CREATE_CONTACT_US = Db.GetSQLInsertForDapperGuid(new Query<ContactUs>(Format.MySQL).Insert());
		private readonly string SQL_GET_CONTACT_BY_ID = new Query<ContactUs>(Format.MySQL).Select().Where("id").GetSql();

		public SupportContactUsService(Db db)
		{
			this._db = db;
		}

		public ContactUs Create(ContactUs form)
		{
			using (var db = this._db.Open())
			{
				return db.Query<ContactUs>(SQL_CREATE_CONTACT_US, form).First();
			}
		}

	}

}
