using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Wsdot.Wzdx.GeoJson.Converters;

namespace Wsdot.Wzdx.GeoJson.Geometries
{
    /// <summary>
    /// GeoJSON Point Geometry
    /// </summary>
    /// <remarks>https://geojson.org/schema/Point.json</remarks>
    public sealed class Point : IGeometry
    {
        [JsonProperty("type", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        [Required]
        public GeometryType Type { get; }
            = GeometryType.Point;

        [JsonProperty("coordinates", Required = Required.Always)]
        [JsonConverter(typeof(PositionConverter))]
        public IPosition Coordinates { get; set; }
        
        [JsonProperty("bbox", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [MaxLength(4)]
        [MinLength(4)]
        public ICollection<double> BoundaryBox { get; set; }
    }
}