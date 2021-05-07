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
            {"Casting Time", "CastingTime"},
            {"Range/Area", "Range"},
            {"Components", "Components"},
            {"Duration", "Duration"},
            {"School", "SchoolOfMagic"},
            {"Attack/Save", "AttackType"},
            {"Damage/Effect", "DamageAndEffectType"},
        };

        public static string CreateSpellFromScraping(string pageName)
        {
            var url = CallSpellPage(pageName);
            var spellName = GatherHTML.ParseHtml(url, "h1", "class", "page-title").FirstOrDefault();
            var spellDetails = GrabSpellDetails(url);

            string details =
                spellDetails.Aggregate("", (current, detail) => current + $"'{detail.Key}' : '{detail.Value}',");

            string descriptionSection = GrabSpellDescription(url);
            bool hasAdditionalInfo = descriptionSection.Contains('*');
            string description = $"'Description' : '{(hasAdditionalInfo ? descriptionSection.Split("*")[0] : descriptionSection)}',";
            string additionInfo = $"'AdditionalInfo' : '{(hasAdditionalInfo ? ("*" + descriptionSection.Split("*")[1]) : "")}',";
            
            string listOfClasses = $"'ListOfClassesSpellIsIn' : [{GrabClassSpellListTags(url)}],";
            string spellTags = $"'SpellTags' : [{GrabSpellTags(url)}]";
            return "{" +
                   $"'Name' : '{spellName.InnerText.Replace('\n', ' ').Trim()}', " +
                   $"{details}" +
                   $"{description}" +
                   $"{additionInfo}" +
                   $"{listOfClasses}" +
                   $"{spellTags}" +
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
                var label = GatherHTML.GetElementContent(detailNode.Descendants()
                    .First(node => node.HasClass("ddb-statblock-item-label")));
                var value = GatherHTML.GetElementContent(detailNode.Descendants()
                    .First(node => node.HasClass("ddb-statblock-item-value")));
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

            var words = detailNodes.FirstOrDefault()?.InnerText.Split(' ');

            if (words != null)
                foreach (var word in words)
                {
                    if (!string.IsNullOrWhiteSpace(word) && word != "\n")
                    {
                        description += word + " ";
                    }
                }

            return description.Replace("\'", "");
        }

        static string GrabSpellTags(string html)
        {
            return TagListAsString(GrabTags(html,"spell-tag"));
        }

        static string GrabClassSpellListTags(string html)
        {
            return TagListAsString(GrabTags(html, "class-tag"));
        }

        static string TagListAsString(List<string> tags)
        {
            var tagListAsString = "";
            for (var index = 0; index < tags.Count; index++)
            {
                var tag = tags[index];
                tagListAsString += $"'{tag}'{(index == tags.Count - 1 ? "" : ", ")}";
            }

            return tagListAsString;
        }

        static List<string> GrabTags(string html, string fliterClassName)
        {
            var detailNodes = GatherHTML.ParseHtml(
                html, "span", "class", fliterClassName);

            return (from detailNode in detailNodes select detailNode.InnerText).ToList();
        }
    }
}