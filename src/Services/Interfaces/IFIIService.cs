using StatusInvestScraping.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StatusInvestScraping.Services
{
    public interface IFIIService
    {
        Task<FII> ObterFII(string fii);
    }
}
