using System.Data.Entity.ModelConfiguration;

namespace ProvaMeta.Model.EntityConfigurations
{
    public class EstadoEntityConfiguration: EntityTypeConfiguration<Estado>
    {
        public EstadoEntityConfiguration()
        {
            this.HasKey(x => x.EstadoId);
        }
    }
}
