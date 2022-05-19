using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Wsdot.Wzdx.GeoJson.Geometries;

namespace Wsdot.Wzdx.GeoJson.Converters
{
    public class GeometryConverter : JsonConverter
    {
        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            return;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                    return ReadGeoJson(JObject.Load(reader));
                case JsonToken.StartArray:
                    return ReadGeoJsonCollection(JArray.Load(reader));
                case JsonToken.Null:
                    return null;
                default:
                    throw new ArgumentException("Unexpected token found");
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IGeometry).IsAssignableFrom(objectType);
        }

        private static IReadOnlyCollection<IGeometry> ReadGeoJsonCollection(JArray values)
        {
            return values.Cast<JObject>().Select(ReadGeoJson).ToList().AsReadOnly();
        }

        private static IGeometry ReadGeoJson(JObject value)
        {
            if (!value.TryGetValue("type", StringComparison.OrdinalIgnoreCase, out var token))
                throw new JsonReaderException("json must contain a \"type\" property");
            if (!Enum.TryParse<GeometryType>(token.Value<string>(), true, out var result))
                throw new JsonReaderException("type must be a supported geo-json geometry object type");

            switch (result)
            {
                case GeometryType.None:
                    return value.ToObject<NullGeometry>();
                case GeometryType.Point:
                    return value.ToObject<Point>();
                case GeometryType.MultiPoint:
                    return value.ToObject<MultiPoint>();
                case GeometryType.LineString:
                    return  value.ToObject<LineString>();
                default:
                    throw new NotSupportedException("Type is not a supported Geometry objects");
            }
        }
    }
}