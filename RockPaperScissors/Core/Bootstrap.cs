namespace RockPaperScissors.Core
{

	public enum BootstrapAlertType : uint
	{
		UNKNOWN = 0,
		INFO = 1,
		DANGER = 2,
		WARNING = 3,
		SUCCESS = 4,
	}

	public static class Bootstrap
	{

		public static string ConvertAlertTypeToString(BootstrapAlertType type)
		{
			switch (type)
			{
				case BootstrapAlertType.DANGER:
					return "alert-danger";
				case BootstrapAlertType.INFO:
					return "alert-info";
				case BootstrapAlertType.WARNING:
					return "alert-warning";
				case BootstrapAlertType.SUCCESS:
					return "alert-success";
				default:
				case BootstrapAlertType.UNKNOWN:
					return string.Empty;
			}
		}

	}

}
