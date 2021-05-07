using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DND.Data
{
    public class SpellLevelConverter : JsonConverter
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
        
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue((string)value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return spellLevelDictionary[(string)reader.Value];
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}