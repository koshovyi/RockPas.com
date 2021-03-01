using Dapper;
using RockPaperScissors.Database;
using RockPaperScissors.Services.Support.Models;
using SqlBuilder;
using SqlBuilder.Linq;
using System;
using System.Linq;

namespace RockPaperScissors.Services.Support
{

	public class SupportArticleService : ISupportArticleService
	{

		private readonly Db _db;

		private readonly string SQL_CREATE_ARTICLE = Db.GetSQLInsertForDapperGuid(new Query<Article>(Format.MySQL).Insert());
		private readonly string SQL_GET_ARTICLE_BY_ID = new Query<Article>(Format.MySQL).Select().Where("id").GetSql();
		private readonly string SQL_GET_ARTICLE_BY_LINK = new Query<Article>(Format.MySQL).Select().Where("link").GetSql();
		private readonly string SQL_UPDATE_ARTICLE = new Query<Article>(Format.MySQL).Update().Where("id").GetSql();

		public SupportArticleService(Db db)
		{
			this._db = db;
		}

		public Article Create(Article article)
		{
			using (var db = this._db.Open())
			{
				return db.Query<Article>(SQL_CREATE_ARTICLE, article).First();
			}
		}

		public Article Get(Guid id)
		{
			using (var db = this._db.Open())
			{
				return db.Query<Article>(SQL_GET_ARTICLE_BY_ID, new { id }).First();
			}
		}

		public Article Get(string link)
		{
			using (var db = this._db.Open())
			{
				return db.Query<Article>(SQL_GET_ARTICLE_BY_LINK, new { link }).First();
			}
		}

		public void Update(Article article)
		{
			using (var db = this._db.Open())
			{
				db.Execute(SQL_UPDATE_ARTICLE, article);
			}
		}
	}

}
