using ProvaMeta.Model;
using ProvaMeta.Model.EntityConfigurations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ProvaMeta.Repositorio
{
    public class ProvaMetaDbContext : DbContext
    {
        public ProvaMetaDbContext() : base ("name=ProvaMetaDbContext")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new EstadoEntityConfiguration());
            modelBuilder.Configurations.Add(new CidadeEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Estado> Estados { get; set; }
    }
}
