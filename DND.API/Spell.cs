using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using WebScrapper.HTML;

namespace DND.API
{
    public static class Spell
    {
        const string SPELL_URL_ROOT = "https://www.dndbeyond.com/spells";

        static Dictionary<string, string> spellFormat = new Dictionary<string, string>
        {
            {"Level", "Level"},
            {"Casting Time","CastingTime"},
            {"Range/Area","Range"},
            {"Components","Components"},
            {"Duration","Duration"},
            {"School","SchoolOfMagic"},
            {"Attack/Save","AttackType"},
            {"Damage/Effect","DamageType"},
        };
        
        public static string CreateSpellFromScraping(string pageName)
        {
            var url = CallSpellPage(pageName);
            var spellName = GatherHTML.ParseHtml(url, "h1", "class", "page-title").FirstOrDefault();
            var spellDetails = GrabSpellDetails(url);
            
            string details = spellDetails.Aggregate("", (current, detail) => current + $"'{detail.Key}' : '{detail.Value}',");

            return "{" +
                       $"'Name' : '{spellName.InnerText.Replace('\n', ' ').Trim()}'," +
                       $"{details}" +
                       $"'Description' :'{GrabSpellDescription(url)}'," +
                       "}";
        }

        static string CallSpellPage(string pageName)
        {
            return GatherHTML.CallUrl(SPELL_URL_ROOT + "/" + pageName).Result;
        }
        
        static Dictionary<string, string> GrabSpellDetails(string html)
        {
            var detailNodes = GatherHTML.ParseHtml(
                html, "div", "class", "ddb-statblock-item")
                .Where(node => node.HasClass("ddb-statblock-item")).ToList();

            var spellDetails = new Dictionary<string, string>();

            foreach (var detailNode in detailNodes)
            {
                var label = GatherHTML.GetElementContent(detailNode.Descendants().First(node => node.HasClass("ddb-statblock-item-label")));
                var value = GatherHTML.GetElementContent(detailNode.Descendants().First(node => node.HasClass("ddb-statblock-item-value")));
                var fix = value.Split(' ');
                value = "";
                foreach (var word in fix)
                {
                    value += string.IsNullOrWhiteSpace(word) ? "" : word + " ";
                }
                spellDetails.Add(spellFormat[label.Replace('\n', ' ').Trim()], value.Replace('\n', ' ').Trim());
            }

            return spellDetails;
        }

        static string GrabSpellDescription(string html)
        {
            var detailNodes = GatherHTML.ParseHtml(
                html, "div", "class", "more-info-content");

            string description = "";

            foreach (var detailNode in detailNodes)
            {
                description += detailNode.InnerText.Replace('\n', ' ').Trim() + "\n";
            }

            return description;
        }

        static List<string> GrabSpellTags(string html)
        {
            return GrabTags(html, "spell-tags", "spell-tag");
        }

        static List<string> GrabClassSpellListTags(string html)
        {
            return GrabTags(html, "availiable-for", "class-tag");
        }
        
        static List<string> GrabTags(string html, string fliterClassName, string nodeHasClassName)
        {
            var detailNodes = GatherHTML.ParseHtml(
                html, "div", "class", fliterClassName);

            return (from detailNode in detailNodes
                where detailNode.HasClass(nodeHasClassName)
                select detailNode.InnerText).ToList();
        }
    }
}