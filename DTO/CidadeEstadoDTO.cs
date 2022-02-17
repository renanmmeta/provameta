using System;
using System.Collections.Generic;

namespace ProvaMeta.DTO
{
    public class CidadeEstadoDTO
    {
        public int CidadeId { get; set; }
        public string NomeCidade { get; set; }
        public string NomePrefeito { get; set; }
        public int QuantidadeVereadores { get; set; }

        public Int64 Habitantes { get; set; }
        public string EstadoNome { get; set; }


        public int EstadoId { get; set; }
        
        public string NomeGovernador { get; set; }
        public int QuantidadeDeputados { get; set; }

        public List<CidadeEstadoDTO> Cidades { get; set; }

        public Int64 TotalPoliticos { get; set; }
        public Int64 TotalHabitantes { get; set; }
    }
}
