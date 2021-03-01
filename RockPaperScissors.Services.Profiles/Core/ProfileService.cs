using Dapper;
using RockPaperScissors.Database;
using SqlBuilder;
using SqlBuilder.Linq;
using System;
using System.Linq;

namespace RockPaperScissors.Services.Profiles
{

	public class ProfileService : IProfileService
	{

		private readonly Db _db;

		private readonly string SQL_CREATE_PROFILE = Db.GetSQLInsertForDapperGuid(new Query<Models.Profile>(Format.MySQL).Insert());
		private readonly string SQL_GET_PROFILE_BY_ID = new Query<Models.Profile>(Format.MySQL).Select().Where("id").GetSql();
		private readonly string SQL_GET_PROFILE_BY_IDS = new Query<Models.Profile>(Format.MySQL).Select().Where(w => w.Raw("id IN @ids")).GetSql();
		private readonly string SQL_GET_PROFILE_BY_LOGIN = new Query<Models.Profile>(Format.MySQL).Select().Where("login").GetSql();
		private readonly string SQL_UPDATE_PROFILE = new Query<Models.Profile>(Format.MySQL).Update().Where("id").GetSql();

		private readonly string SQL_UPDATE_EMAIL = new Query<Models.Profile>(Format.MySQL).Update().Sets("email").Where("id").GetSql();
		private readonly string SQL_UPDATE_PASSWORD = new Query(Format.MySQL).Update(Reflection.GetTableName<Models.Profile>()).Sets("password").Where("id").GetSql();

		private readonly string SQL_EXIST_EMAIL = Db.GetSQLSelectExist(new Query<Models.Profile>(Format.MySQL).Select().Where("email"));
		private readonly string SQL_EXIST_LOGIN = Db.GetSQLSelectExist(new Query<Models.Profile>(Format.MySQL).Select().Where("login"));

		private readonly string SQL_FBID_EXIST = Db.GetSQLSelectExist(new Query<Models.Profile>(Format.MySQL).Select().Where("facebookid"));
		private readonly string SQL_FBID_GET = new Query<Models.Profile>(Format.MySQL).Select().Where("facebookid").GetSql();
		private readonly string SQL_GOOGLEID_EXIST = Db.GetSQLSelectExist(new Query<Models.Profile>(Format.MySQL).Select().Where("googleid"));
		private readonly string SQL_GOOGLEID_GET = new Query<Models.Profile>(Format.MySQL).Select().Where("googleid").GetSql();

		private readonly string SQL_UPDATE_ONLINE = new Query(Format.MySQL).Update(Reflection.GetTableName<Models.Profile>()).Sets("onlineat").Where("id").GetSql();

		public ProfileService(Db db)
		{
			this._db = db;
		}

		public Models.Profile Create(Models.Profile profile)
		{
			using (var db = this._db.Open())
			{
				return db.Query<Models.Profile>(SQL_CREATE_PROFILE, profile).First();
			}
		}

		public Models.Profile Get(Guid id)
		{
			using (var db = this._db.Open())
			{
				return db.Query<Models.Profile>(SQL_GET_PROFILE_BY_ID, new { id }).First();
			}
		}

		public Models.Profile Get(string login)
		{
			using (var db = this._db.Open())
			{
				return db.Query<Models.Profile>(SQL_GET_PROFILE_BY_LOGIN, new { login }).First();
			}
		}

		public Models.Profile[] Get(Guid[] ids)
		{
			using (var db = this._db.Open())
			{
				return db.Query<Models.Profile>(SQL_GET_PROFILE_BY_IDS, new { ids }).ToArray();
			}
		}

		public Models.Profile GetByFacebookId(string facebookId)
		{
			using (var db = this._db.Open())
			{
				return db.Query<Models.Profile>(SQL_FBID_GET, new { facebookId }).First();
			}
		}

		public Models.Profile GetByGoogleId(string googleId)
		{
			using (var db = this._db.Open())
			{
				return db.Query<Models.Profile>(SQL_GOOGLEID_GET, new { googleId }).First();
			}
		}

		public void ChangeEmail(Guid id, string email)
		{
			using (var db = this._db.Open())
			{
				db.Execute(SQL_UPDATE_EMAIL, new { id, email });
			}
		}

		public void ChangePassword(Guid id, string password)
		{
			using (var db = this._db.Open())
			{
				db.Execute(SQL_UPDATE_PASSWORD, new { id, password });
			}
		}

		public bool ExistEmail(string email)
		{
			using (var db = this._db.Open())
			{
				return db.Query<bool>(SQL_EXIST_EMAIL, new { email }).First();
			}
		}

		public bool ExistLogin(string login)
		{
			using (var db = this._db.Open())
			{
				return db.Query<bool>(SQL_EXIST_LOGIN, new { login }).First();
			}
		}

		public bool CheckFacebookId(string facebookId)
		{
			using (var db = this._db.Open())
			{
				return db.Query<bool>(SQL_FBID_EXIST, new { facebookId }).First();
			}
		}

		public bool CheckGoogleId(string googleId)
		{
			using (var db = this._db.Open())
			{
				return db.Query<bool>(SQL_GOOGLEID_EXIST, new { googleId }).First();
			}
		}

		public void SetOnline(Guid id)
		{
			using (var db = this._db.Open())
			{
				db.Execute(SQL_UPDATE_ONLINE, new { onlineat = DateTime.UtcNow, id });
			}
		}

		public void StatusActivate(Guid id, bool status)
		{
			throw new NotImplementedException();
		}

		public void StatusConfirm(Guid id, bool status)
		{
			throw new NotImplementedException();
		}

		public void StatusEnable(Guid id, bool status)
		{
			throw new NotImplementedException();
		}

		public void Update(Models.Profile profile)
		{
			using (var db = this._db.Open())
			{
				db.Execute(SQL_UPDATE_PROFILE, profile);
			}
		}

	}

}
