namespace ToDoAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isCompleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Todos", "IsCompleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Todos", "IsCompleted");
        }
    }
}
