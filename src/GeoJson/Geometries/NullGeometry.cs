using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wsdot.Wzdx.GeoJson.Geometries
{
    /// <summary>
    /// Instance of geometry that does not contain any coordinates
    /// </summary>
    public sealed  class NullGeometry : IGeometry
    {
        [JsonProperty("type", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public GeometryType Type { get; } = GeometryType.None;
    }
}