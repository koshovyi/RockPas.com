using FluentMigrator;

namespace RockPaperScissors.Database.Database.Migrations
{

	[Migration(202010162118)]//format yyyymmddhhmm
	public partial class Migration_M0_Genesis : Migration
	{

		//only forward
		public override void Down()
		{
		}

		public override void Up()
		{
			this.Profiles();
			this.Common();
			this.Support();
			this.Games();
		}

	}

}
