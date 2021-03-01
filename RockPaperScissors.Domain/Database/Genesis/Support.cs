namespace RockPaperScissors.Database.Database.Migrations
{

	public partial class Migration_M0_Genesis
	{

		private void Support()
		{
			this.SupportContactUs();
			this.SupportArticles();
		}

		private void SupportContactUs()
		{
			this.Create.Table(Constants.TABLE_SUPPORT_CONTACT_US)
				//id
				.WithColumn("id").AsGuid().PrimaryKey().NotNullable()
				//fk
				.WithColumn("id_profile").AsGuid().ForeignKey(Constants.TABLE_PROFILES, "id").Nullable()
				//dates
				.WithColumn("createdat").AsDateTime().NotNullable()
				.WithColumn("readedat").AsDateTime().Nullable()
				//strings
				.WithColumn("name").AsString(32).NotNullable()
				.WithColumn("contacts").AsString(240).Nullable()
				.WithColumn("memo").AsCustom("text").NotNullable()
				.WithColumn("ip").AsString(39).NotNullable()
				.WithColumn("useragent").AsCustom("text").NotNullable()
				//bools
				.WithColumn("isanswered").AsBoolean().WithDefaultValue(false).NotNullable()
				.WithColumn("isreaded").AsBoolean().WithDefaultValue(false).NotNullable()
				.WithColumn("isdeleted").AsBoolean().WithDefaultValue(false).NotNullable();
		}

		private void SupportArticles()
		{
			this.Create.Table(Constants.TABLE_SUPPORT_ARTICLES)
				//id
				.WithColumn("id").AsGuid().PrimaryKey().NotNullable()
				//strings
				.WithColumn("link").AsString(128).Unique().NotNullable()
				.WithColumn("title").AsString(256).NotNullable()
				.WithColumn("memo").AsCustom("text").NotNullable();
		}

	}

}
