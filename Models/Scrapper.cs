using HtmlAgilityPack;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Xml;

namespace WebScrapper.Models
{
    public class Scrapper
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var httpClient = new HttpClient();
            var query = "Jackson X Series Rhoads RRX24 PRPL guitar";
            var searchUrl = $"https://www.google.com/search?q={Uri.EscapeDataString(query)}"; // Sustituir con la API de búsqueda si es posible

            var response = await httpClient.GetStringAsync(searchUrl);
            var doc = new HtmlDocument();
            doc.LoadHtml(response);

            // Extraer URLs de los resultados de búsqueda
            var nodes = doc.DocumentNode.SelectNodes("//a[position() <= 10/@href]");

            foreach (var node in nodes)
            {
                // Aquí node contiene el valor del atributo href
                var href = node.InnerText; // o node.Value;
                Console.WriteLine(href);

                var detailPageUrl = node.GetAttributeValue("href", "");

                // Obtener y procesar cada página dentro del enlace correspondinete del foreach
                var detailResponse = await httpClient.GetStringAsync(detailPageUrl);
                var detailDoc = new HtmlDocument();
                detailDoc.LoadHtml(detailResponse);

                // Aquí aplicar XPath para extraer datos específicos de cada página
                // Ejemplo: var precio = detailDoc.DocumentNode.SelectSingleNode("//xpath_para_precio").InnerText;
            }
        }
    }
}
