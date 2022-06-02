using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        public IEnumerable<double> BoundaryBox { get; set; }

        /// <summary>
        /// Create Point Geometry instance from a single position coordinate value
        /// </summary>
        /// <param name="values">Sequence of position coordinate values containing only one value</param>
        /// <returns>Instance of Point Geometry comprised of coordinates</returns>
        public static Point FromCoordinates(IEnumerable<IPosition> values)
        {
            return FromCoordinates(values.Single());
        }

        public static Point FromCoordinates(IPosition value)
        {
            return new Point()
            {
                Coordinates = value,
                BoundaryBox = new [] { value , value}.AsBoundaryBox().ToList().AsReadOnly()
            };
        }
    }
}