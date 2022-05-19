using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wsdot.Wzdx.GeoJson.Geometries
{
    public sealed  class NullGeometry : IGeometry
    {
        [JsonProperty("type", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public GeometryType Type { get; } = GeometryType.None;
    }
}