namespace NextWorld.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trainees",
                c => new
                    {
                        TraineeNo = c.Int(nullable: false, identity: true),
                        TraineeName = c.String(nullable: false, maxLength: 40),
                        TraineeEmail = c.String(nullable: false, maxLength: 40),
                        TraineeCourse = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TraineeNo);
        }
        public override void Down()
        {
            DropTable("dbo.Trainees");
        }
    }
}
