using CsQuery;
using StatusInvestScraping.Exceptions;
using StatusInvestScraping.Helpers;
using StatusInvestScraping.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StatusInvestScraping.Services
{
    public class FIIService : ServiceBase,IFIIService
    {
        public FIIService()
        {
            
        }

        public async Task<FII> ObterFII(string fii)
        {
            try
            {
                if (string.IsNullOrEmpty(fii))
                    throw new ArgumentException("Código de FII vazio.");

                var url = string.Format(Constantes.StatusInvestWeb,
                    "fundos-imobiliarios", fii);

                var html = await RetornaHtml(url);
                return await Task.Run(() => ConverteHtmlPacote(html, url));
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        private FII ConverteHtmlPacote(string html, string url)
        {
            var fundo = new FII();
            try
            {
                CQ cq = html;

                //Fundo
                fundo.InicioFundo = cq["#fund-section > div > div > div:nth-child(2) > div > div:nth-child(3) > div > div > strong"].Text();
                fundo.ValorPatrimonialCota = cq["#main-2 > div.container.pb-7 > div:nth-child(3) > div > div:nth-child(1) > div > div:nth-child(1) > strong"].Text();
                fundo.Pvp = cq["#main-2 > div.container.pb-7 > div:nth-child(3) > div > div:nth-child(2) > div > div:nth-child(1) > strong"].Text(); 

                //Ativo
                fundo.Url = url;
                fundo.Ticket = cq["#main-header > div > div > div:nth-child(1) > div > ol > li:nth-child(3) > a > span"].Text();
                fundo.Tipo = cq["#main-header > div > div > div:nth-child(1) > div > ol > li:nth-child(2) > a > span"].Text();
                fundo.Nome = cq["#main-header > div > div > div:nth-child(1) > h1 > small"].Text();
                fundo.Cnpj = cq["#fund-section > div > div > div:nth-child(2) > div > div:nth-child(1) > div > div > strong"].Text();

                fundo.ValorAtual.Valor = cq["#main-2 > div.container.pb-7 > div.top-info.d-flex.flex-wrap.justify-between.mb-3.mb-md-5 > div.info.special.w-100.w-md-33.w-lg-20 > div > div:nth-child(1) > strong"].Text();
                fundo.ValorAtual.Variacao = cq["#main-2 > div.container.pb-7 > div.top-info.d-flex.flex-wrap.justify-between.mb-3.mb-md-5 > div.info.special.w-100.w-md-33.w-lg-20 > div > div.w-lg-100 > span > b"].Text();
                fundo.ValorPatrimonialCota = cq["#main-2 > div.container.pb-7 div.mb-5 > div.top-info.top-info-2.top-info-md-3.top-info-lg-n.d-flex.justify-between > div.info > div > div:nth-child(1) > strong"].Text();
                fundo.DividendYield = cq["#main-2 > div.container.pb-7 > div.top-info.d-flex.flex-wrap.justify-between.mb-3.mb-md-5 > div:nth-child(4) > div > div:nth-child(1) > strong"].Text();


                //Comparativo do Dividendo
                fundo.Dividendo.AnoPassado = cq["#earning-section > div.list > div > div > div:nth-child(1) > div > div > strong"].Text();
                fundo.Dividendo.AnoAtual = cq["#earning-section > div.list > div > div > div:nth-child(2) > div > div > strong"].Text();

                if (cq["#earning-section > div.list > div > div > div:nth-child(3) > div > div > i"].Text() == "arrow_downward")
                {
                    fundo.Dividendo.ComparacaoPositiva = false;
                    fundo.Dividendo.Sinal = "-";
                }
                else
                {
                    fundo.Dividendo.ComparacaoPositiva = true;
                    fundo.Dividendo.Sinal = "+";
                }

                fundo.Dividendo.Comparacao = $"{fundo.Dividendo.Sinal} {cq["#earning-section > div.list > div > div > div:nth-child(3) > div > div > strong"].Text()}";
                fundo.Dividendo.Provisionado = cq["#earning-section > div.list > div > div > div:nth-child(4) > div > div > strong"].Text();
                fundo.Dividendo.ComparacaoProvisionado = cq["#earning-section > div.list > div > div > div:nth-child(5) > div > div > strong"].Text();

                var dividendos = cq.Select("#results").First().Select("et").Text();

                return fundo;
            }
            catch (InexistenteException inexistenteException)
            {
                fundo.IsValid = false;
                fundo.Observacao = inexistenteException.Message;
                return fundo;
            }
            catch (Exception exception)
            {
                fundo.IsValid = false;
                fundo.Observacao = exception.Message;
                return fundo;
            }
        }

    }
    
}
