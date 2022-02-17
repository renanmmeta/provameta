using System.Collections.Generic;

namespace ProvaMeta.Model
{
    public class Estado
    {
        public int EstadoId { get; set; }
        public string Nome { get; set; }
        public string NomeGovernador { get; set; }
        public int QuantidadeDeputados { get; set; }
        public virtual List<Cidade> Cidades { get; set; }

    }
}
