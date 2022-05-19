using System;
using Newtonsoft.Json;
using Wsdot.Wzdx.GeoJson.Geometries;

namespace Wsdot.Wzdx.GeoJson.Converters
{
    internal class PositionConverter : JsonConverter
    {
        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            double[] coordinates;
            try
            {
                coordinates = serializer.Deserialize<double[]>(reader);
            }
            catch (Exception ex)
            {
                throw new JsonReaderException("error parsing coordinates", ex);
            }

            if (coordinates == null) throw new JsonReaderException("coordinates cannot be null");

            var enumerator = coordinates.GetEnumerator();

            var longitude = enumerator.MoveNext()
                ? enumerator.Current as double? ?? 0
                : throw new ArgumentOutOfRangeException(nameof(coordinates),
                    "To few coordinates, expected two or three coordinates");
            var latitude = enumerator.MoveNext()
                ? enumerator.Current as double? ?? 0
                : throw new ArgumentOutOfRangeException(nameof(coordinates),
                    "To few coordinates, expected two or three coordinates");
            if (enumerator.MoveNext() && enumerator.MoveNext())
                throw new ArgumentOutOfRangeException(nameof(coordinates),
                    "To many coordinates, expected two or three coordinates");

            return new Position(longitude, latitude);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var position = value as IPosition;
            if (position == null)
                throw new ArgumentException("Expected IPosition type", nameof(value));

            writer.WriteStartArray();
            writer.WriteValue(position.Longitude);
            writer.WriteValue(position.Latitude);
            writer.WriteEndArray();
        }
        
        public override bool CanConvert(Type objectType)
        {
            return typeof(IPosition).IsAssignableFrom(objectType);
        }
    }
}