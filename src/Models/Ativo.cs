using System;
using System.Collections.Generic;
using System.Text;

namespace StatusInvestScraping.Models
{
    public class Ativo
    {
        public string Url { get; internal set; }
        public bool IsValid { get; internal set; }
        public string Observacao { get; internal set; }
        public string Ticket { get; internal set; }
        public string Tipo { get; internal set; }
        public string Nome { get; internal set; }
        public string Cnpj { get; internal set; }
        public string PrecoAtual { get; internal set; }
        public string Valor { get; internal set; }
        public string Variacao { get; internal set; }
        public string DividendYield { get; internal set; }
        public Dividendo Dividendo { get; internal set; }

        public ValorAtual ValorAtual { get; internal set; }

        public Ativo()
        {
            Dividendo = new Dividendo();
            ValorAtual = new ValorAtual();
        }
    }
}
