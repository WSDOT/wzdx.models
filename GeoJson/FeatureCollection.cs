using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wsdot.Wzdx.GeoJson
{
    [DataContract]
    public abstract class FeatureCollection<T>
        where T : IFeature
    {
        [JsonProperty("type", Required = Required.Always)]
        [DataMember(Name = "type", IsRequired = true)]
        [JsonConverter(typeof(StringEnumConverter))]
        public FeatureType Type { get; set; }
            = FeatureType.FeatureCollection;

        [JsonProperty("features", Required = Required.Always)]
        [DataMember(Name = "features", IsRequired = true)]
        public ICollection<T> Features { get; set; } = new Collection<T>();

        [JsonProperty("bbox", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [DataMember(Name = "bbox")]
        [MinLength(4)]
        public ICollection<double> Bbox { get; set; }

    }
}
