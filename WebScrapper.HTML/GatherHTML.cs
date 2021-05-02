using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebScrapper.HTML
{
    public class GatherHTML
    {
        public static Task<string> CallUrl(string fullURL)
        {
            HttpClient client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
            client.DefaultRequestHeaders.Accept.Clear();
            var response = client.GetStringAsync(fullURL);
            return response;
        }

        public static List<HtmlNode> ParseHtml(string html, string nodeFilter, string attribute, string nameOfAttribute)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var detailNodes = htmlDocument.DocumentNode.Descendants(nodeFilter)
                .Where(node => node.GetAttributeValue(attribute, "").Contains(nameOfAttribute)).ToList();

            return detailNodes;
        }

        public static string GetJson(List<string> information)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var info in information)
            {
                var json = JsonSerializer.Serialize(info);
                sb.AppendLine(json);
            }

            return sb.ToString();
        }

        public static List<HtmlNode> FilterHtmlNodes(List<HtmlNode> nodes, string className, string containerName)
        {
            var spanList = nodes.Find(node => node.GetAttributeValue("class", "").Contains(className))
                ?.Descendants(containerName).ToList();

            return spanList;
        }

        public static string GetAttributeName(HtmlNode node, string attribute)
        {
            return node.GetAttributeValue(attribute,"");
        }

        public static void WriteNamesToFile(List<string> names)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var name in names)
            {
                sb.AppendLine(name);
            }

            File.WriteAllText("AccessorNames.csv", sb.ToString());
        }
    }
}
