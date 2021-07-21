namespace ToDoAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creatingdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Todos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TodoData = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Todos");
        }
    }
}
