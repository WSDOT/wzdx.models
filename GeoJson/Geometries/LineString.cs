using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Wsdot.Wzdx.GeoJson.Converters;

namespace Wsdot.Wzdx.GeoJson.Geometries
{
    /// <summary>
    /// GeoJSON LineString Geometry
    /// </summary>
    /// <remarks>https://geojson.org/schema/LineString.json</remarks>
    public sealed class LineString : IGeometry
    {
        [JsonProperty("type", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        [Required]
        public GeometryType Type { get; }
            = GeometryType.LineString;

        [JsonProperty("coordinates", Required = Required.Always)]
        [JsonConverter(typeof(EnumerablePositionConverter))]
        [MinLength(2)]
        [Required]
        public IReadOnlyCollection<IPosition> Coordinates { get; set; }

        [JsonProperty("bbox", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [MaxLength(4)]
        [MinLength(4)]
        public IEnumerable<double> BoundaryBox { get; set; }
    }
}