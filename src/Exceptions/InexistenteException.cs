using System;
using System.Collections.Generic;
using System.Text;

namespace StatusInvestScraping.Exceptions
{
    public class InexistenteException : Exception
    {
        public override string Message => "Ativo inexistente.";
    }
}
