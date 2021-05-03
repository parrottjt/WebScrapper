using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlAgilityPack;
using WebScrapper.HTML;

namespace DND.API
{
    public static class HtmlConversion
    {
        public static List<string> SpellAccessorNames { get; private set; }

        static void GetAccessorNames()
        {
            if(SpellAccessorNames != null) return;
            try
            {
                SpellAccessorNames = File.ReadAllLines("AccessorNames.csv").ToList();
            }
            catch (Exception e)
            {
                SpellAccessorNames = new List<string>();
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
