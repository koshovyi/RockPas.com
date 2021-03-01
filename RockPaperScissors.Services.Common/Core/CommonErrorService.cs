using Dapper;
using RockPaperScissors.Database;
using RockPaperScissors.Services.Common.Models;
using SqlBuilder;
using System.Linq;

namespace RockPaperScissors.Services.Common
{

	public class CommonErrorService : ICommonErrorService
	{

		private readonly Db _db;

		private readonly string SQL_CREATE_ERROR = Db.GetSQLInsertForDapperGuid(new Query<Error>(Format.MySQL).Insert());

		public CommonErrorService(Db db)
		{
			this._db = db;
		}

		public Error Append(Error error)
		{
			using (var db = this._db.Open())
			{
				return db.Query<Error>(SQL_CREATE_ERROR, error).First();
			}
		}

	}

}
