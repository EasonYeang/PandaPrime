namespace Utility.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PAccount",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Account = c.String(maxLength: 64),
                        UserName = c.String(maxLength: 64),
                        Password = c.String(maxLength: 64),
                        NickName = c.String(maxLength: 64),
                        IsActive = c.Boolean(),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PPermission",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 256),
                        SerialNumber = c.Int(nullable: false),
                        ParentSN = c.Int(),
                        Level = c.Int(),
                        FilePath = c.String(unicode: false, storeType: "text"),
                        Icon = c.String(maxLength: 128),
                        Order = c.Int(),
                        IsDelete = c.Boolean(),
                        AddTime = c.DateTime(),
                        LastTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PPermission");
            DropTable("dbo.PAccount");
        }
    }
}
