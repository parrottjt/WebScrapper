using System;
using System.Linq;
using Newtonsoft.Json;

namespace DND.Data
{
    public class DamageAndEffectTypeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            DamageAndEffectType attackAndEffectType = (DamageAndEffectType) value;
            writer.WriteValue(attackAndEffectType.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var enumString = (string) reader.Value;
            var concatString = string.Concat(enumString.Split(' ')[0].Where(c => !char.IsWhiteSpace(c)));
            return Enum.Parse<DamageAndEffectType>(concatString.Split(' ')[0], true);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}