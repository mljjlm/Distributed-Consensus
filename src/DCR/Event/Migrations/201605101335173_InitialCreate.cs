namespace Event.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConditionUris",
                c => new
                    {
                        WorkflowId = c.String(nullable: false, maxLength: 128),
                        EventId = c.String(nullable: false, maxLength: 128),
                        ForeignEventId = c.String(nullable: false, maxLength: 128),
                        UriString = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkflowId, t.EventId, t.ForeignEventId })
                .ForeignKey("dbo.EventModels", t => new { t.WorkflowId, t.EventId }, cascadeDelete: true)
                .Index(t => new { t.WorkflowId, t.EventId });
            
            CreateTable(
                "dbo.EventModels",
                c => new
                    {
                        WorkflowId = c.String(nullable: false, maxLength: 128),
                        Id = c.String(nullable: false, maxLength: 128),
                        OwnUri = c.String(),
                        Name = c.String(),
                        Executed = c.Boolean(nullable: false),
                        Included = c.Boolean(nullable: false),
                        Pending = c.Boolean(nullable: false),
                        LockOwner = c.String(),
                        IsEvil = c.Boolean(nullable: false),
                        InitialPending = c.Boolean(nullable: false),
                        InitialExecuted = c.Boolean(nullable: false),
                        InitialIncluded = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkflowId, t.Id });
            
            CreateTable(
                "dbo.ExclusionUris",
                c => new
                    {
                        WorkflowId = c.String(nullable: false, maxLength: 128),
                        EventId = c.String(nullable: false, maxLength: 128),
                        ForeignEventId = c.String(nullable: false, maxLength: 128),
                        UriString = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkflowId, t.EventId, t.ForeignEventId })
                .ForeignKey("dbo.EventModels", t => new { t.WorkflowId, t.EventId }, cascadeDelete: true)
                .Index(t => new { t.WorkflowId, t.EventId });
            
            CreateTable(
                "dbo.InclusionUris",
                c => new
                    {
                        WorkflowId = c.String(nullable: false, maxLength: 128),
                        EventId = c.String(nullable: false, maxLength: 128),
                        ForeignEventId = c.String(nullable: false, maxLength: 128),
                        UriString = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkflowId, t.EventId, t.ForeignEventId })
                .ForeignKey("dbo.EventModels", t => new { t.WorkflowId, t.EventId }, cascadeDelete: true)
                .Index(t => new { t.WorkflowId, t.EventId });
            
            CreateTable(
                "dbo.MilestoneUris",
                c => new
                    {
                        WorkflowId = c.String(nullable: false, maxLength: 128),
                        EventId = c.String(nullable: false, maxLength: 128),
                        ForeignEventId = c.String(nullable: false, maxLength: 128),
                        UriString = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkflowId, t.EventId, t.ForeignEventId })
                .ForeignKey("dbo.EventModels", t => new { t.WorkflowId, t.EventId }, cascadeDelete: true)
                .Index(t => new { t.WorkflowId, t.EventId });
            
            CreateTable(
                "dbo.ResponseUris",
                c => new
                    {
                        WorkflowId = c.String(nullable: false, maxLength: 128),
                        EventId = c.String(nullable: false, maxLength: 128),
                        ForeignEventId = c.String(nullable: false, maxLength: 128),
                        UriString = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkflowId, t.EventId, t.ForeignEventId })
                .ForeignKey("dbo.EventModels", t => new { t.WorkflowId, t.EventId }, cascadeDelete: true)
                .Index(t => new { t.WorkflowId, t.EventId });
            
            CreateTable(
                "dbo.EventRoleModels",
                c => new
                    {
                        WorkflowId = c.String(nullable: false, maxLength: 128),
                        EventId = c.String(nullable: false, maxLength: 128),
                        Role = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.WorkflowId, t.EventId, t.Role })
                .ForeignKey("dbo.EventModels", t => new { t.WorkflowId, t.EventId }, cascadeDelete: true)
                .Index(t => new { t.WorkflowId, t.EventId });
            
            CreateTable(
                "dbo.CheatingTypes",
                c => new
                    {
                        WorkflowId = c.String(nullable: false, maxLength: 128),
                        EventId = c.String(nullable: false, maxLength: 128),
                        Type = c.Int(nullable: false),
                        Event_WorkflowId = c.String(maxLength: 128),
                        Event_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.WorkflowId, t.EventId, t.Type })
                .ForeignKey("dbo.EventModels", t => new { t.Event_WorkflowId, t.Event_Id })
                .Index(t => new { t.Event_WorkflowId, t.Event_Id });
            
            CreateTable(
                "dbo.ActionModels",
                c => new
                    {
                        Timestamp = c.Int(nullable: false),
                        EventId = c.String(nullable: false, maxLength: 128),
                        WorkflowId = c.String(nullable: false, maxLength: 128),
                        CounterpartId = c.String(),
                        CounterpartTimeStamp = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Timestamp, t.EventId, t.WorkflowId });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ConditionUris", new[] { "WorkflowId", "EventId" }, "dbo.EventModels");
            DropForeignKey("dbo.CheatingTypes", new[] { "Event_WorkflowId", "Event_Id" }, "dbo.EventModels");
            DropForeignKey("dbo.EventRoleModels", new[] { "WorkflowId", "EventId" }, "dbo.EventModels");
            DropForeignKey("dbo.ResponseUris", new[] { "WorkflowId", "EventId" }, "dbo.EventModels");
            DropForeignKey("dbo.MilestoneUris", new[] { "WorkflowId", "EventId" }, "dbo.EventModels");
            DropForeignKey("dbo.InclusionUris", new[] { "WorkflowId", "EventId" }, "dbo.EventModels");
            DropForeignKey("dbo.ExclusionUris", new[] { "WorkflowId", "EventId" }, "dbo.EventModels");
            DropIndex("dbo.CheatingTypes", new[] { "Event_WorkflowId", "Event_Id" });
            DropIndex("dbo.EventRoleModels", new[] { "WorkflowId", "EventId" });
            DropIndex("dbo.ResponseUris", new[] { "WorkflowId", "EventId" });
            DropIndex("dbo.MilestoneUris", new[] { "WorkflowId", "EventId" });
            DropIndex("dbo.InclusionUris", new[] { "WorkflowId", "EventId" });
            DropIndex("dbo.ExclusionUris", new[] { "WorkflowId", "EventId" });
            DropIndex("dbo.ConditionUris", new[] { "WorkflowId", "EventId" });
            DropTable("dbo.ActionModels");
            DropTable("dbo.CheatingTypes");
            DropTable("dbo.EventRoleModels");
            DropTable("dbo.ResponseUris");
            DropTable("dbo.MilestoneUris");
            DropTable("dbo.InclusionUris");
            DropTable("dbo.ExclusionUris");
            DropTable("dbo.EventModels");
            DropTable("dbo.ConditionUris");
        }
    }
}
