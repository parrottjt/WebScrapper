using System;
using System.Linq;
using Newtonsoft.Json;

namespace DND.Data
{
    public class AttackTypeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            AttackType attackType = (AttackType) value;
            writer.WriteValue(attackType.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var enumString = (string) reader.Value;
            var concatString = string.Concat(enumString.Where(c => !char.IsWhiteSpace(c)));
            return Enum.Parse<AttackType>(concatString, true);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}