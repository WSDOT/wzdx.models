using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Wzdx.v4.WorkZones.Converters
{
    public class RoadEventConverter : JsonConverter
    {
        private static IEnumerable<IRoadEventConverter> RoadEventConverters { get; }
            = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(IRoadEventConverter).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(type => (IRoadEventConverter)Activator.CreateInstance(type))
                .ToList()
                .AsReadOnly();

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var roadEvent = value as IRoadEvent;
            if (roadEvent == null)
                throw new ArgumentException("Expected IRoadEvent type", nameof(value));

            serializer.Serialize(writer, roadEvent);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                    return ReadRoadEvent(JObject.Load(reader));
                default:
                    throw new ArgumentException("Unexpected token found");
            }
        }

        private static IRoadEvent ReadRoadEvent(JObject value)
        {
            // TryReadCoreDetails
            if (!value.TryGetValue("core_details", StringComparison.OrdinalIgnoreCase, out var detailsToken))
                throw new JsonReaderException("json must contain \"core_details\" property");

            var details = detailsToken.ToObject<RoadEventCoreDetails>();
            if (details == null)
                throw new JsonReaderException("invalid \"core_details\" value");

            // TryGetConverter
            var converter = RoadEventConverters.FirstOrDefault(item => item.CanConvert(details));
            if (converter == null)
                throw new NotSupportedException($"Event type \"{details.EventType}\" not supported");

            return converter.Read(value);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IRoadEvent).IsAssignableFrom(objectType);
        }
    }
}