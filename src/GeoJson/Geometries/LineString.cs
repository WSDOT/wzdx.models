using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        /// <summary>
        /// Create LineString Geometry instance from position coordinate values
        /// </summary>
        /// <param name="value">Sequence of position coordinate values</param>
        /// <returns>Instance of LineString Geometry comprised of coordinates</returns>
        public static LineString FromCoordinates(IEnumerable<IPosition> value)
        {
            var coordinates = value.ToList().AsReadOnly();
            return new LineString()
            {
                Coordinates = coordinates,
                BoundaryBox = coordinates.AsBoundaryBox().ToList().AsReadOnly()
            };
        }
    }
}