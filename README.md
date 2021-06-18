# StatusInvestScraping

 Web Scraping do site StatusInvest para Ações e Fundos Imobiliarios
 
 
###### This is the component, works on .NET Core and.NET Framework

**NuGet**

|Name|Info|Contributors|
| ------------------- | ------------------- | ------------------- |
|StatusInvestScraping|[![NuGet](https://buildstats.info/nuget/StatusInvestScraping)](https://www.nuget.org/packages/StatusInvestScraping/)|[![GitHub contributors](https://img.shields.io/github/contributors/TBertuzzi/StatusInvestScraping.svg)](https://github.com/TBertuzzi/StatusInvestScraping/graphs/contributors)|

Exemplo

```csharp

static void Main(string[] args)
        {
            IAcaoService acaoService = new AcaoService();
            IFIIService fiiService = new FIIService();

            var resultAcao = acaoService.ObterAcao("trpl4").Result;

            System.Console.WriteLine("Inicio:");
            System.Console.WriteLine("");
            System.Console.WriteLine("Ação:");
            System.Console.WriteLine($"Nome: {resultAcao.Nome}");
            System.Console.WriteLine($"Tipo: {resultAcao.Tipo}");
            System.Console.WriteLine($"Ticket: {resultAcao.Ticket}");
            System.Console.WriteLine($"Cnpj: {resultAcao.Cnpj}");
            System.Console.WriteLine($"ValorAtual.Valor: {resultAcao.ValorAtual.Valor}");
            System.Console.WriteLine($"ValorAtual.Variacao: {resultAcao.ValorAtual.Variacao}");
            System.Console.WriteLine($"DividendYield: {resultAcao.DividendYield}");


            var resultFundo = fiiService.ObterFII("mxrf11").Result;
            System.Console.WriteLine("");
            System.Console.WriteLine("");
            System.Console.WriteLine("FII:");
            System.Console.WriteLine($"Nome: {resultFundo.Nome}");
            System.Console.WriteLine($"Tipo: {resultFundo.Tipo}");
            System.Console.WriteLine($"Ticket: {resultFundo.Ticket}");
            System.Console.WriteLine($"Cnpj: {resultFundo.Cnpj}");
            System.Console.WriteLine($"ValorAtual.Valor: {resultFundo.ValorAtual.Valor}");
            System.Console.WriteLine($"ValorAtual.Variacao: {resultFundo.ValorAtual.Variacao}");
            System.Console.WriteLine($"Pvp: {resultFundo.Pvp}");
            System.Console.WriteLine($"DividendYield: {resultFundo.DividendYield}");
            System.Console.WriteLine($"InicioFundo: {resultFundo.InicioFundo}");
            System.Console.WriteLine($"ValorPatrimonialCota: {resultFundo.ValorPatrimonialCota}");

        }

```
