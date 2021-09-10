namespace EdgeRealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addVet : DbMigration
    {
        public override void Up()
        {
            
            
           
            CreateTable(
                "dbo.CustomerSelectFlats",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        FlatId = c.Int(nullable: false),
                        CurrntlyDate = c.DateTime(),
                        CustomerPaiedId = c.Int(nullable: false),
                        CostMony = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Vet = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustPaperPayment_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.CustPaperPayments", t => t.CustPaperPayment_id)
                .ForeignKey("dbo.Flat", t => t.FlatId)
                .Index(t => t.CustomerId)
                .Index(t => t.FlatId)
                .Index(t => t.CustPaperPayment_id);
            
           
            
           
            
           
            
            
        }
        
        public override void Down()
        {
            
        }
    }
}
