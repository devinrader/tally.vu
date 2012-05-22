namespace tallyvu.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class baseline : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Polls",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        PhoneNumber = c.String(),
                        Key = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Options",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Text = c.String(),
                        Shortcut = c.String(),
                        Key = c.String(nullable: false, maxLength: 50),
                        Poll_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Polls", t => t.Poll_Id)
                .Index(t => t.Poll_Id);
            
            CreateTable(
                "Votes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        From = c.String(),
                        Value = c.String(),
                        Key = c.String(nullable: false, maxLength: 50),
                        Poll_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Polls", t => t.Poll_Id)
                .Index(t => t.Poll_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("Votes", new[] { "Poll_Id" });
            DropIndex("Options", new[] { "Poll_Id" });
            DropForeignKey("Votes", "Poll_Id", "Polls");
            DropForeignKey("Options", "Poll_Id", "Polls");
            DropTable("Votes");
            DropTable("Options");
            DropTable("Polls");
        }
    }
}
