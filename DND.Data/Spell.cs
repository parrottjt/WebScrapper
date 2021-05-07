using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;

namespace DND.Data
{
    public class Spell
    {
        public string AccessorName { get; private set; }
        public string Name { get; private set; }
        [JsonConverter(typeof(SpellLevelConverter))] public int Level { get; private set; }
        public string CastingTime { get; private set; }
        public string Range { get; private set; }
        public string Components { get; private set; }
        public string Duration { get; private set; }
        public SchoolOfMagic SchoolOfMagic { get; private set; }
        [JsonConverter(typeof(AttackTypeConverter))] public AttackType AttackType { get; private set; }
        [JsonConverter(typeof(DamageAndEffectTypeConverter))]public DamageAndEffectType DamageAndEffectType { get; private set; }
        public string Description { get; private set; }
        public string AdditionalInfo { get; private set; }
        public List<string> ListOfClassesSpellIsIn { get; private set; }
        public List<string> SpellTags { get; private set; }
        public Spell(string name = "", 
            int level = 0,
            string castingTime ="",
            string range = "",
            string components = "",
            string duration = "",
            AttackType attackType = default,
            DamageAndEffectType damageAndEffectType = default, 
            SchoolOfMagic schoolOfMagic = default,
            string description = "",
            string additionalInfo = "",
            List<string> listOfClassesSpellIsIn = null,
            List<string> spellTags = null)
        {
            AccessorName = CreateAccessorName(name);
            Name = name;
            Level = Math.Clamp(level, 0, 9);
            CastingTime = castingTime;
            Range = range;
            Components = components;
            Duration = duration;
            AttackType = attackType;
            DamageAndEffectType = damageAndEffectType;
            SchoolOfMagic = schoolOfMagic;
            Description = description;
            AdditionalInfo = additionalInfo;
            ListOfClassesSpellIsIn = listOfClassesSpellIsIn ?? new List<string>();
            SpellTags = spellTags ?? new List<string>();
        }

        string CreateAccessorName(string name)
        {
            var noSpaces = name.Replace(' ', '-');
            string accessorName = String.Concat(noSpaces.Where(character => character != '\''));
            return accessorName.ToLower();
        }
    }
}
