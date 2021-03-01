using Dapper;
using RockPaperScissors.Database;
using RockPaperScissors.Services.Common.Models;
using SqlBuilder;
using System.Linq;

namespace RockPaperScissors.Services.Common
{

	public class CommonLogsService : ICommonLogsService
	{


		private readonly Db _db;

		private readonly string SQL_CREATE_ERROR = Db.GetSQLInsertForDapperGuid(new Query<Log>(Format.MySQL).Insert());

		public CommonLogsService(Db db)
		{
			this._db = db;
		}

		public Log Append(Log log)
		{
			using (var db = this._db.Open())
			{
				return db.Query<Log>(SQL_CREATE_ERROR, log).First();
			}
		}

	}

}
