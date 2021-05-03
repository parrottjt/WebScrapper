using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DND.Data
{
    public class Spell
    {
        Dictionary<string, int> spellLevelDictionary = new Dictionary<string, int>
        {
            {"Cantrip", 0},
            {"1st", 1},
            {"2nd", 2},
            {"3rd", 3},
            {"4th", 4},
            {"5th", 5},
            {"6th", 6},
            {"7th", 7},
            {"8th", 8},
            {"9th", 9},
        };
        
        public string AccessorName { get; private set; }
        public string Name { get; private set; }
        public int Level { get; private set; }
        public string CastingTime { get; private set; }
        public string Range { get; private set; }
        public string Components { get; private set; }
        public string Duration { get; private set; }
        public SchoolOfMagic SchoolOfMagic { get; private set; }
        public AttackType AttackType { get; private set; }
        public DamageType DamageType { get; private set; }
        public string Description { get; private set; }
        public string MaterialComponents { get; private set; }
        public List<string> ListOfClassesSpellIsIn { get; private set; }

        public Spell(string name = "", string level = "Cantrip", string description = "", string components = "",
            DamageType damageType = default, SchoolOfMagic schoolOfMagic = default, List<string> listOfClassesSpellIsIn = null)
        {
            AccessorName = CreateAccessorName(name);
            Name = name;
            Level = spellLevelDictionary[level];
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
