using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebScrapper.DND.Data
{
    public class Spell
    {
        const int MIN_SPELL_LEVEL = 0;
        const int MAX_SPELL_LEVEL = 9;

        public string AccessorName { get; private set; }
        public string Name { get; private set; }
        public int Level { get; private set; }
        public string Description { get; private set; }
        public string Components { get; private set; }
        public DamageType DamageType { get; private set; }
        public SchoolOfMagic SchoolOfMagic { get; private set; }

        public List<string> ListOfClassesSpellIsIn { get; private set; }

        public Spell(string name = "", int level = 0, string description = "", string components = "",
            DamageType damageType = default, SchoolOfMagic schoolOfMagic = default, List<string> listOfClassesSpellIsIn = null)
        {
            AccessorName = CreateAccessorName(name);
            Name = name;
            Level = Math.Clamp(level, MIN_SPELL_LEVEL, MAX_SPELL_LEVEL);
            Description = description;
            Components = components;
            DamageType = damageType;
            ListOfClassesSpellIsIn = listOfClassesSpellIsIn ?? new List<string>();
            SchoolOfMagic = schoolOfMagic;
        }

        string CreateAccessorName(string name)
        {
            var noSpaces = name.Replace(' ', '-');
            string accessorName = String.Concat(noSpaces.Where(character => character != '\''));
            return accessorName.ToLower();
        }
    }
}
