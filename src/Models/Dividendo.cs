using System;
using System.Collections.Generic;
using System.Text;

namespace StatusInvestScraping.Models
{
    public class Dividendo
    {
        public string Descricao { get; set; }
        public string AnoPassado { get; set; }
        public string AnoAtual { get; set; }
        public string Comparacao { get; set; }
        public bool ComparacaoPositiva { get; internal set; }
        public string Sinal { get; internal set; }
        public string Provisionado { get; internal set; }
        public string ComparacaoProvisionado { get; internal set; }
    }
}
