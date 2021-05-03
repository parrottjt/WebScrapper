using System.Diagnostics;
using DND.API;
using Newtonsoft.Json;
using NUnit.Framework;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;

namespace WebScrapper.Test
{
    public class DndAPITests
    {
        [Test]
        public void Create_Spell_FromWebpage()
        {
            var result = Spell.CreateSpellFromScraping("fireball");
            dynamic data = JsonConvert.DeserializeObject<global::DND.Data.Spell>(result);
            
            System.Console.Out.Write(data);
        }
    }
}