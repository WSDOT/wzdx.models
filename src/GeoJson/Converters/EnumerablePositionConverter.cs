using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Wzdx.GeoJson.Geometries;

namespace Wzdx.GeoJson.Converters
{
    internal class EnumerablePositionConverter : JsonConverter
    {
        private static readonly PositionConverter PositionConverter = new PositionConverter();

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var source = existingValue as JArray ?? serializer.Deserialize<JArray>(reader);

            if (source == null)
                throw new ArgumentException("Unsupported value type, expected sequence of coordinates.", nameof(existingValue));

            return source.Select(pos =>
                    PositionConverter.ReadJson(pos.CreateReader(),
                        typeof(IPosition),
                        pos,
                        serializer))
                .Cast<IPosition>()
                .ToList()
                .AsReadOnly();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var positions = value as IEnumerable<IPosition>;
            if (positions == null)
                throw new ArgumentException("Unsupported value type, expected sequence of positions.", nameof(value));

            writer.WriteStartArray();
            foreach (var position in positions)
                PositionConverter.WriteJson(writer, position, serializer);

            writer.WriteEndArray();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IEnumerable<IPosition>).IsAssignableFrom(objectType);
        }
    }
}