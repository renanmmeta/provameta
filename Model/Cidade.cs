using System;

namespace ProvaMeta.Model
{
    public class Cidade
    {
        public int CidadeId { get; set; }
        public string Nome { get; set; }

        public string NomePrefeito { get; set; }

        public int QuantidadeVereadores { get; set; }

        public Int64 Habitantes { get; set; }

        public int EstadoId { get; set; }
        public virtual Estado Estado { get; set; }
    }
}
