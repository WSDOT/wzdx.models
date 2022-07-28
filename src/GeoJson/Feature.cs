using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Wzdx.GeoJson.Converters;
using Wzdx.GeoJson.Geometries;

namespace Wzdx.GeoJson
{
    public interface IFeature
    {
        string Id { get; }
        FeatureType Type { get; }
        IGeometry Geometry { get; }
        ICollection<double> BoundaryBox { get; }
    }

    public abstract class Feature<TProperties> : IFeature
    {
        /// <summary>
        /// A unique identifier used to identify the feature
        /// </summary>
        [JsonProperty("id", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public FeatureType Type { get; protected set; }
            = FeatureType.Feature;

        [JsonProperty("properties", Required = Required.Always)]
        [Required]
        public abstract TProperties Properties { get; set; }

        [JsonProperty(PropertyName = "geometry", Required = Required.AllowNull)]
        [JsonConverter(typeof(GeometryConverter))]
        public IGeometry Geometry { get; set; }

        [JsonProperty("bbox", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [MaxLength(4)]
        [MinLength(4)]
        public ICollection<double> BoundaryBox { get; set; }
    }
}