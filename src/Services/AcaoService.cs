using CsQuery;
using StatusInvestScraping.Exceptions;
using StatusInvestScraping.Helpers;
using StatusInvestScraping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StatusInvestScraping.Services
{
    public class AcaoService : IAcaoService
    {
        private readonly HttpClient _httpClient;
        public AcaoService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<Acao> ObterAcao(string acao)
        {
            try
            {
                if (string.IsNullOrEmpty(acao))
                    throw new ArgumentException("Código de Ação vazio.");

                var url = string.Format(Constantes.StatusInvestWeb, "acoes", acao);
                var response = await _httpClient.GetByteArrayAsync(url);
                var html = Encoding.GetEncoding("ISO-8859-1").GetString(response, 0, response.Length - 1);
                return await Task.Run(() => ConverteHtmlPacote(html, url));
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        private Acao ConverteHtmlPacote(string html, string url)
        {
            var acao = new Acao();
            try
            {
                CQ cq = html;

                acao.Url = url;
                acao.Ticket = cq["#main-header > div > div > div:nth-child(1) > div > ol > li:nth-child(3) > a > span"].Text();
                acao.Tipo = cq["#main-header > div > div > div:nth-child(1) > div > ol > li:nth-child(2) > a > span"].Text();
                acao.Nome = cq["#main-header > div > div > div:nth-child(1) > h1 > small"].Text();
                acao.Cnpj = cq["#company-section > div > div.d-block.d-md-flex.mb-5.img-lazy-group > div.company-description.w-100.w-md-70.ml-md-5 > h4 > small"].Text();
                acao.ValorAtual.Valor = cq["#main-2 > div:nth-child(4) > div > div.pb-3.pb-md-5 > div > div.info.special.w-100.w-md-33.w-lg-20 > div > div:nth-child(1) > strong"].Text();
                acao.ValorAtual.Variacao = cq["#main-2 > div:nth-child(4) > div > div.pb-3.pb-md-5 > div > div.info.special.w-100.w-md-33.w-lg-20 > div > div.w-lg-100 > span > b"].Text();
                acao.DividendYield = cq["#main-2 > div:nth-child(4) > div > div.pb-3.pb-md-5 > div > div:nth-child(4) > div > div:nth-child(1) > strong"].Text();

                //Comparativo do Dividendo
                acao.Dividendo.AnoPassado = cq["#earning-section > div.list > div > div > div:nth-child(1) > div > div > strong"].Text();
                acao.Dividendo.AnoAtual = cq["#earning-section > div.list > div > div > div:nth-child(2) > div > div > strong"].Text();

                if(cq["#earning-section > div.list > div > div > div:nth-child(3) > div > div > i"].Text() == "arrow_downward")
                {
                  acao.Dividendo.ComparacaoPositiva = false;
                  acao.Dividendo.Sinal = "-";
                }
                else
                {
                    acao.Dividendo.ComparacaoPositiva = true;
                    acao.Dividendo.Sinal = "+";
                }

                acao.Dividendo.Comparacao = $"{acao.Dividendo.Sinal} {cq["#earning-section > div.list > div > div > div:nth-child(3) > div > div > strong"].Text()}";
                acao.Dividendo.Provisionado = cq["#earning-section > div.list > div > div > div:nth-child(4) > div > div > strong"].Text();
                acao.Dividendo.ComparacaoProvisionado = cq["#earning-section > div.list > div > div > div:nth-child(5) > div > div > strong"].Text();

                var dividendos = cq.Select("#results").First().Select("et").Text(); 

                return acao;
            }
            catch (InexistenteException inexistenteException)
            {
                acao.IsValid = false;
                acao.Observacao = inexistenteException.Message;
                return acao;
            }
            catch (Exception exception)
            {
                acao.IsValid = false;
                acao.Observacao = exception.Message;
                return acao;
            }
        }


    }
}
