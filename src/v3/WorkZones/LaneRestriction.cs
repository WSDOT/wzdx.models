using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wsdot.Wzdx.v3.WorkZones
{
    /// <summary>A lane-level restriction, including type and value</summary>

    public class LaneRestriction 
    {
        [JsonProperty("restriction_type", Required = Required.Always)]
        public RoadRestriction RestrictionType { get; set; }

        [JsonProperty("restriction_value", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double? RestrictionValue { get; set; }

        [JsonProperty("restriction_units", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public LaneRestrictionUnit? RestrictionUnits { get; set; }

        private IDictionary<string, object> _additionalProperties = new Dictionary<string, object>();

        [JsonExtensionData]
        public IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties; }
            set { _additionalProperties = value; }
        }


    }
}