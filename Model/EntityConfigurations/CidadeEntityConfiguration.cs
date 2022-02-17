using System.Data.Entity.ModelConfiguration;

namespace ProvaMeta.Model.EntityConfigurations
{
    public class CidadeEntityConfiguration : EntityTypeConfiguration<Cidade>
    {
        public CidadeEntityConfiguration()
        {
            this.HasKey(x => x.CidadeId);
            this.HasRequired(x => x.Estado)
                .WithMany(x =>x.Cidades)
                .HasForeignKey(x =>x.EstadoId)
                .WillCascadeOnDelete(false);
        }
    }
}
