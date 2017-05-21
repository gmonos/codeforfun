using System.Data.Entity.Migrations;

namespace Zags.OrganizationService.DB.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Organization.Organization",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Reference = c.String(),
                        RaisonSociale = c.String(),
                        FormeJuridique = c.String(),
                        Effectif = c.Int(),
                        SIRET = c.String(),
                        CodeNAF = c.String(),
                        IdentifiantConventionCollective = c.String(),
                        Activite = c.String(),
                        Telephone1 = c.String(),
                        Telephone2 = c.String(),
                        NumeroFax = c.String(),
                        Email = c.String(),
                        SiteWeb = c.String(),
                        AggregateId = c.Guid(nullable: false),
                        Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Organization.Address",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategorieAdresse = c.Int(nullable: false),
                        NumVoie = c.String(),
                        NomVoie = c.String(),
                        ComplementVoie = c.String(),
                        CodePostal = c.String(),
                        Ville = c.Int(nullable: false),
                        Pays = c.Int(nullable: false),
                        TypeVoie = c.Int(nullable: false),
                    AggregateId = c.Guid(nullable: false),
                    Version = c.Int(nullable: false),
                    Organization_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Organization.Organization", t => t.Organization_Id)
                .Index(t => t.Organization_Id);
            
            CreateTable(
                "Organization.Iban",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulaire = c.String(),
                        IBAN = c.String(),
                        BIC = c.String(),
                        Banque = c.Int(nullable: false),
                    AggregateId = c.Guid(nullable: false),
                    Version = c.Int(nullable: false),
                    Organization_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Organization.Organization", t => t.Organization_Id)
                .Index(t => t.Organization_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Organization.Iban", "Organization_Id", "Organization.Organization");
            DropForeignKey("Organization.Address", "Organization_Id", "Organization.Organization");
            DropIndex("Organization.Iban", new[] { "Organization_Id" });
            DropIndex("Organization.Address", new[] { "Organization_Id" });
            DropTable("Organization.Iban");
            DropTable("Organization.Address");
            DropTable("Organization.Organization");
        }
    }
}
