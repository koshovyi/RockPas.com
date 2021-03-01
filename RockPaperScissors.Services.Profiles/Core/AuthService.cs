using Dapper;
using RockPaperScissors.Database;
using SqlBuilder;
using SqlBuilder.Linq;
using System;
using System.Linq;

namespace RockPaperScissors.Services.Profiles
{

	public class AuthService : IAuthService
	{

		private readonly Db _db;

		private readonly string SQL_CHECK_LOGIN_PASSWORD = Db.GetSQLSelectExist(new Query<Models.Profile>(Format.MySQL).Select().Where("login", "password"));
		private readonly string SQL_GETID_LOGIN_PASSWORD = new Query<Models.Profile>(Format.MySQL).Select().Columns("id").Where("login", "password").GetSql();

		public AuthService(Db db)
		{
			this._db = db;
		}

		public bool CheckLoginAndPassword(string login, string password)
		{
			using (var db = this._db.Open())
			{
				return db.Query<bool>(SQL_CHECK_LOGIN_PASSWORD, new { login, password }).First();
			}
		}

		public Guid GetIdByLoginAndPassword(string login, string password)
		{
			using (var db = this._db.Open())
			{
				return db.Query<Guid>(SQL_GETID_LOGIN_PASSWORD, new { login, password }).FirstOrDefault();
			}
		}

	}

}
