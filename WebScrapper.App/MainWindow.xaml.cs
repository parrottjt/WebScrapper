using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DND.API;
using HtmlAgilityPack;
using WebScrapper.HTML;

namespace WebScrapper.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly string url = "https://www.dndbeyond.com/spells";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void WebPageDataClick(object sender, RoutedEventArgs e)
        {
            // List<string> accessorNames = new List<string>();
            // StringBuilder sb = new StringBuilder();
            //
            // for (int pageIndex = 1; pageIndex <= 26; pageIndex++)
            // {
            //     HtmlString.Text = $"Reading Page {pageIndex} of 26";
            //     var response = GatherHTML.CallUrl(url + $"?page={pageIndex}").Result;
            //     var result = GatherHTML.ParseHtml(response, "div", "class", "info");
            //     accessorNames.AddRange(result.Select(node => GatherHTML.GetAttributeName(node, "data-slug")));
            //     foreach (var node in result)
            //     {
            //         sb.AppendLine(GatherHTML.GetAttributeName(node, "data-slug"));
            //     }
            // }
            //
            // HtmlString.Text = sb.ToString();
            // GatherHTML.WriteNamesToFile(accessorNames);
            // File.Open("AccessorNames.csv", FileMode.Open);

            var text = Spell.CreateSpellFromScraping("fireball");
            HtmlString.Text = text;
        }
    }
}