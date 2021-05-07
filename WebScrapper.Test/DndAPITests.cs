using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using DND.Data;
using Newtonsoft.Json;
using NUnit.Framework;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Spell = DND.API.Spell;

namespace WebScrapper.Test
{
    public class DndAPITests
    {
        [Test]
        public void Create_Spell_FromWebpage()
        {
            var result = Spell.CreateSpellFromScraping("fireball");

            dynamic data = JsonConvert.DeserializeObject<DND.Data.Spell>(result);
            
            var fileText = new StringBuilder();
            fileText.AppendLine(JsonSerializer.Serialize(data, new JsonSerializerOptions {WriteIndented = true}));

            File.WriteAllText((Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/Test.text")
                , fileText.ToString());

            Assert.AreEqual("Fireball", data.Name);
        }
    }
}