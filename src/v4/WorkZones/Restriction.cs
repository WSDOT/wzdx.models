using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wsdot.Wzdx.v4.WorkZones
{
    /// <summary>
    /// A restriction on a roadway or lane, including type and value
    /// </summary>
    public class Restriction
    {
        [JsonProperty("type", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public RestrictionType Type { get; set; }

        [JsonProperty("value", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double? Value { get; set; }

        [JsonProperty("unit", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public UnitOfMeasurement? Unit { get; set; }
    }
}