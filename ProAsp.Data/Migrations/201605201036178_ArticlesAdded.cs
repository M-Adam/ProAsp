namespace ProAsp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticlesAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        Content = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        Creator_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Creator_Id)
                .Index(t => t.Creator_Id);
            
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "Creator_Id", "dbo.Users");
            DropIndex("dbo.Articles", new[] { "Creator_Id" });
            AlterColumn("dbo.Users", "Name", c => c.String());
            DropTable("dbo.Articles");
        }
    }
}
