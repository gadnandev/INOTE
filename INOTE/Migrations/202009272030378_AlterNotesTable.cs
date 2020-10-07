namespace INOTE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterNotesTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Notes", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Notes", "Content", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notes", "Content", c => c.String());
            AlterColumn("dbo.Notes", "Title", c => c.String());
        }
    }
}
