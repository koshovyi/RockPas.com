namespace RockPaperScissors.Database.Database.Migrations
{

	public partial class Migration_M0_Genesis
	{

		private void Games()
		{
			this.Create.Table(Constants.TABLE_GAMES)
				//id
				.WithColumn("id").AsGuid().PrimaryKey().NotNullable()
				//fk
				.WithColumn("id_session").AsGuid().NotNullable()
				//dates
				.WithColumn("createdat").AsDateTime().NotNullable()
				.WithColumn("finishedat").AsDateTime().Nullable()
				//enums
				.WithColumn("handauthor").AsInt32().WithDefaultValue(0).NotNullable()
				.WithColumn("handopponent").AsInt32().WithDefaultValue(0).NotNullable()
				.WithColumn("result").AsInt32().WithDefaultValue(0).NotNullable()
				//strings
				.WithColumn("nameauthor").AsString(21).NotNullable()
				.WithColumn("nameopponent").AsString(21).NotNullable()
				.WithColumn("trophy").AsString(64).Nullable()
				.WithColumn("memo").AsCustom("text").Nullable();
		}

	}

}
