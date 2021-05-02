using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using NUnit.Framework;
using WebScrapper.HTML;

namespace WebScrapper.Test
{
    class HTTPRequestTests
    {
        string url;

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GetWebPageHTML()
        {
            url = "https://www.dndbeyond.com/";
            Assert.IsNotEmpty(GatherHTML.CallUrl(url).Result);
        }

        [Test]
        public void ParseHtmlElementsFromHtmlPage()
        {
            url = "https://www.dndbeyond.com/classes/barbarian";
            var response = GatherHTML.CallUrl(url);
            Assert.IsNotEmpty(GatherHTML.ParseHtml(response.Result,"div","class","subitems-list-details-item"));

            url = "https://www.dndbeyond.com/spells";
            response = GatherHTML.CallUrl(url);
            Assert.IsNotEmpty(GatherHTML.ParseHtml(response.Result,"div","class","info"));
        }
    }
}
