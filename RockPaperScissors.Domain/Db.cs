using MySqlConnector;
using SqlBuilder.Interfaces;
using System;

namespace RockPaperScissors.Database
{

	public class Db
	{

		public string ConnectionString { get; }

		public Db(string connectionString)
		{
			this.ConnectionString = connectionString;
		}

		public MySqlConnection Open()
		{
			MySqlConnection db = new MySqlConnection(this.ConnectionString);
			db.Open();
			return db;
		}

		public static string GetSQLInsertForDapperGuid(IStatementInsert sql) =>
			"SET @uuid = UUID();" + Environment.NewLine + sql.GetSql() + Environment.NewLine + $"SELECT * FROM {sql.TableName} WHERE id=@uuid;";

		public static string GetSQLInsertForDapperInt(IStatementInsert sql) =>
			sql.GetSql() + Environment.NewLine + $"SELECT * FROM {sql.TableName} WHERE id=LAST_INSERT_ID();";

		public static string GetSQLSelectExist(IStatementSelect sql) =>
			"SELECT EXISTS (" + sql.GetSql(true) + ");";

		public void Dispose()
		{
		}

	}

}
