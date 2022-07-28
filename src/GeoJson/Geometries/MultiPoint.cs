using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Wzdx.GeoJson.Converters;

namespace Wzdx.GeoJson.Geometries
{
    /// <summary>
    /// GeoJSON MultiPoint Geometry
    /// </summary>
    /// <remarks>https://geojson.org/schema/MultiPoint.json</remarks>
    public sealed class MultiPoint : IGeometry
    {
        [JsonProperty("type", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        [Required]
        public GeometryType Type { get; }
            = GeometryType.MultiPoint;

        [JsonProperty("coordinates", Required = Required.Always)]
        [JsonConverter(typeof(EnumerablePositionConverter))]
        [MinLength(1)]
        [Required]
        public IReadOnlyCollection<IPosition> Coordinates { get; set; }

        [JsonProperty("bbox", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [MaxLength(4)]
        [MinLength(4)]
        public IEnumerable<double> BoundaryBox { get; set; }

        /// <summary>
        /// Create MultiPoint Geometry instance from position coordinate values
        /// </summary>
        /// <param name="value">Sequence of position coordinate values</param>
        /// <returns>Instance of MultiPoint Geometry comprised of coordinates</returns>
        public static MultiPoint FromCoordinates(IEnumerable<IPosition> value)
        {
            var coordinates = value.ToList().AsReadOnly();
            return new MultiPoint()
            {
                Coordinates = coordinates,
                BoundaryBox = coordinates.AsBoundaryBox().ToList().AsReadOnly()
            };
        }
    }
}