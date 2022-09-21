namespace _272ass.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initcom : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastEdit = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Attendees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        Email = c.String(nullable: false),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastEdit = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastEdit = c.DateTime(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        RegistrationStart = c.DateTime(nullable: false),
                        RegistrationEnd = c.DateTime(nullable: false),
                        Location = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        OrganiserID = c.Int(nullable: false),
                        EventTypeID = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EventTypes", t => t.EventTypeID, cascadeDelete: true)
                .ForeignKey("dbo.Organisers", t => t.OrganiserID, cascadeDelete: true)
                .Index(t => t.OrganiserID)
                .Index(t => t.EventTypeID);
            
            CreateTable(
                "dbo.EventTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Organisers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastEdit = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EventAttendees",
                c => new
                    {
                        Event_ID = c.Int(nullable: false),
                        Attendee_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Event_ID, t.Attendee_ID })
                .ForeignKey("dbo.Events", t => t.Event_ID, cascadeDelete: true)
                .ForeignKey("dbo.Attendees", t => t.Attendee_ID, cascadeDelete: true)
                .Index(t => t.Event_ID)
                .Index(t => t.Attendee_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "OrganiserID", "dbo.Organisers");
            DropForeignKey("dbo.Events", "EventTypeID", "dbo.EventTypes");
            DropForeignKey("dbo.EventAttendees", "Attendee_ID", "dbo.Attendees");
            DropForeignKey("dbo.EventAttendees", "Event_ID", "dbo.Events");
            DropIndex("dbo.EventAttendees", new[] { "Attendee_ID" });
            DropIndex("dbo.EventAttendees", new[] { "Event_ID" });
            DropIndex("dbo.Events", new[] { "EventTypeID" });
            DropIndex("dbo.Events", new[] { "OrganiserID" });
            DropTable("dbo.EventAttendees");
            DropTable("dbo.Organisers");
            DropTable("dbo.EventTypes");
            DropTable("dbo.Events");
            DropTable("dbo.Attendees");
            DropTable("dbo.Admins");
        }
    }
}
