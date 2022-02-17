namespace ProvaMeta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cidade",
                c => new
                    {
                        CidadeId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        NomePrefeito = c.String(),
                        QuantidadeVereadores = c.Int(nullable: false),
                        Habitantes = c.Long(nullable: false),
                        EstadoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CidadeId)
                .ForeignKey("dbo.Estado", t => t.EstadoId)
                .Index(t => t.EstadoId);
            
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        EstadoId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        NomeGovernador = c.String(),
                        QuantidadeDeputados = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EstadoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cidade", "EstadoId", "dbo.Estado");
            DropIndex("dbo.Cidade", new[] { "EstadoId" });
            DropTable("dbo.Estado");
            DropTable("dbo.Cidade");
        }
    }
}
