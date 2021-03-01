namespace RockPaperScissors.Database.Database.Migrations
{

	public partial class Migration_M0_Genesis
	{

		private void Common()
		{
			this.CommonErrors();
			this.CommonLogs();
		}

		private void CommonErrors()
		{
			this.Create.Table(Constants.COMMON_ERRORS)
				//id
				.WithColumn("id").AsGuid().PrimaryKey().NotNullable()
				//fk
				.WithColumn("id_profile").AsGuid().ForeignKey(Constants.TABLE_PROFILES, "id").Nullable()
				//dates
				.WithColumn("createdat").AsDateTime().NotNullable()
				//strings
				.WithColumn("path").AsString(256).NotNullable()
				.WithColumn("exception").AsString(32).Nullable()
				.WithColumn("memo").AsCustom("text").NotNullable();
		}

		private void CommonLogs()
		{
			this.Create.Table(Constants.COMMON_LOGS)
				//id
				.WithColumn("id").AsGuid().PrimaryKey().NotNullable()
				//fk
				.WithColumn("id_profile").AsGuid().ForeignKey(Constants.TABLE_PROFILES, "id").Nullable()
				//dates
				.WithColumn("createdat").AsDateTime().NotNullable()
				//int
				.WithColumn("type").AsInt32().WithDefaultValue(0).NotNullable()
				//strings
				.WithColumn("json").AsCustom("text").NotNullable();
		}

	}

}
