using System;
using System.Collections.Generic;
using System.Text;

namespace StatusInvestScraping.Models
{
    public class FII : Ativo
    {
        public string InicioFundo { get; internal set; }
        public string ValorPatrimonialCota { get; internal set; }

        public string Pvp { get; internal set; }
    }
}
