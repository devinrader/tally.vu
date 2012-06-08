namespace tallyvu.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ChangedOption : DbMigration
    {
        public override void Up()
        {
            AddColumn("Options", "Answer", c => c.String());
            DropColumn("Options", "Text");
        }
        
        public override void Down()
        {
            AddColumn("Options", "Text", c => c.String());
            DropColumn("Options", "Answer");
        }
    }
}
