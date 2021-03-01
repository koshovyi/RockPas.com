namespace RockPaperScissors.Database.Database.Migrations
{

	public partial class Migration_M0_Genesis
	{

		private void Profiles()
		{
			this.Create.Table(Constants.TABLE_PROFILES)
				//id
				.WithColumn("id").AsGuid().PrimaryKey().NotNullable()
				//strings
				.WithColumn("login").AsString(24).Unique().NotNullable()
				.WithColumn("password").AsString(64).Nullable()
				.WithColumn("phone").AsString(18).Nullable()
				.WithColumn("email").AsString(120).Nullable()
				.WithColumn("url").AsString(240).Nullable()
				.WithColumn("firstname").AsString(32).Nullable()
				.WithColumn("lastname").AsString(32).Nullable()
				.WithColumn("displayname").AsString(32).NotNullable()
				.WithColumn("memo").AsCustom("text").Nullable()
				//strings social
				.WithColumn("facebookid").AsString(24).Unique().Nullable()
				.WithColumn("facebookdata").AsCustom("text").Nullable()
				.WithColumn("googleid").AsString(24).Unique().Nullable()
				.WithColumn("googledata").AsCustom("text").Nullable()
				//dates
				.WithColumn("createdat").AsDateTime().NotNullable()
				.WithColumn("updatedat").AsDateTime().Nullable()
				.WithColumn("onlineat").AsDateTime().Nullable()
				//bools
				.WithColumn("isverified").AsBoolean().WithDefaultValue(false).NotNullable()
				.WithColumn("isemailconfirmed").AsBoolean().WithDefaultValue(false).NotNullable()
				.WithColumn("isphoneconfirmed").AsBoolean().WithDefaultValue(false).NotNullable()
				.WithColumn("islocked").AsBoolean().WithDefaultValue(true).NotNullable()
				.WithColumn("isdeleted").AsBoolean().WithDefaultValue(false).NotNullable();
		}

	}

}
