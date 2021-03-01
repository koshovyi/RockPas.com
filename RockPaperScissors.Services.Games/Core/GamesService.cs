using Dapper;
using RockPaperScissors.Database;
using RockPaperScissors.Services.Games.Models;
using SqlBuilder;
using SqlBuilder.Linq;
using System;
using System.Linq;

namespace RockPaperScissors.Services.Games
{

	public class GamesService : IGamesService
	{

		private readonly Db _db;

		private readonly string SQL_CREATE_GAME = Db.GetSQLInsertForDapperGuid(new Query<Game>(Format.MySQL).Insert());
		private readonly string SQL_GET_GAME_BY_ID = new Query<Game>(Format.MySQL).Select().Where("id").GetSql();
		private readonly string SQL_EXIST_GAME = Db.GetSQLSelectExist(new Query<Game>(Format.MySQL).Select().Where("id"));
		private readonly string SQL_UPDATE_GAME = new Query<Game>(Format.MySQL).Update().Where("id").GetSql();

		public GamesService(Db db)
		{
			this._db = db;
		}

		public Game Create(Game article)
		{
			using (var db = this._db.Open())
			{
				return db.Query<Game>(SQL_CREATE_GAME, article).First();
			}
		}

		public Game Get(Guid id)
		{
			using (var db = this._db.Open())
			{
				return db.Query<Game>(SQL_GET_GAME_BY_ID, new { id }).First();
			}
		}

		public bool Exist(Guid id)
		{
			using (var db = this._db.Open())
			{
				return db.Query<bool>(SQL_EXIST_GAME, new { id }).First();
			}
		}

		public void Update(Game game)
		{
			using (var db = this._db.Open())
			{
				db.Execute(SQL_UPDATE_GAME, game);
			}
		}
	}

}
