using System;

namespace RockPaperScissors.Core
{

	public class Config
	{

		public Main Main { get; set; }

		public Mysql DataBase { get; set; }

		public Security Security { get; set; }

		public GoogleRecaptcha GoogleRecaptcha { get; set; }

		public Donate Donate { get; set; }

		public Config()
		{
			this.Main = new Main();
			this.DataBase = new Mysql();
			this.Security = new Security();
			this.GoogleRecaptcha = new GoogleRecaptcha();
			this.Donate = new Donate();
		}

	}

	#region Main

	public class Main
	{

		public string Host { get; set; }

	}

	#endregion

	#region DataBase

	public class Mysql
	{

		public string ConnectionString { get; set; }

		public string DbHost { get; set; }

		public string DbName { get; set; }

		public string DbLogin { get; set; }

		public string DbPassword { get; set; }

		public string FormatConnectionString()
		{
			return string.Format(this.ConnectionString, this.DbHost, this.DbName, this.DbLogin, this.DbPassword);
		}

	}

	#endregion

	#region Security

	public class Security
	{

		public string Salt1 { get; set; }

		public string Salt2 { get; set; }

	}

	public class GoogleRecaptcha
	{

		public string ApiKey { get; set; }

		public string ApiSecret { get; set; }

	}

	#endregion

	public class Donate
	{

		public string PayPal { get; set; }

	}

	public class EnvVars
	{

		public static bool IsDevelopment => GetValue("ASPNETCORE_ENVIRONMENT") == "Development";

		public static bool IsLocalhost => GetValue("ASPNETCORE_ENVIRONMENT") == "Localhost";

		public static string GetValue(string name)
		{
			string result = Environment.GetEnvironmentVariable(name);
			if (string.IsNullOrEmpty(result))
				return string.Empty;
			return result;
		}

	}

}
