using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Wsdot.Wzdx.v4.Devices.Converters
{
    public class FieldDeviceFeatureConverter : JsonConverter
    {
        private static IEnumerable<IFieldDeviceConverter> FieldDeviceConverters { get; }
            = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(IFieldDeviceConverter).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(type => (IFieldDeviceConverter)Activator.CreateInstance(type))
                .ToList()
                .AsReadOnly();

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var roadEvent = value as IFieldDevice;
            if (roadEvent == null)
                throw new ArgumentException("Expected IFieldDevice type", nameof(value));

            serializer.Serialize(writer, roadEvent);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                    return ReadFieldDevice(JObject.Load(reader));
                default:
                    throw new ArgumentException("Unexpected token found");
            }
        }

        private static IFieldDevice ReadFieldDevice(JObject value)
        {
            // TryReadCoreDetails
            if (!value.TryGetValue("core_details", StringComparison.OrdinalIgnoreCase, out var detailsToken))
                throw new JsonReaderException("json must contain \"core_details\" property");

            var details = detailsToken.ToObject<FieldDeviceCoreDetails>();
            if (details == null)
                throw new JsonReaderException("invalid \"core_details\" value");

            // TryGetConverter
            var converter = FieldDeviceConverters.FirstOrDefault(item => item.CanConvert(details));
            if (converter == null)
                throw new NotSupportedException($"Device type \"{details.DeviceType}\" not supported");

            return converter.Read(value);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IFieldDevice).IsAssignableFrom(objectType);
        }
    }
}