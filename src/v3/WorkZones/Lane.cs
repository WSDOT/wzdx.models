using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wsdot.Wzdx.v3.WorkZones
{
    /// <summary>An individual lane within a road event</summary>

    public class Lane 
    {
        /// <summary>The position (index) of the lane in sequence on the roadway, where '1' represents the left-most lane</summary>
        [JsonProperty("order", Required = Required.Always)]
        [Range(1, int.MaxValue)]
        public int Order { get; set; }

        [JsonProperty("status", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public LaneStatus Status { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public LaneType Type { get; set; }

        /// <summary>The number assigned to the lane to help identify its position. Flexible, but usually used for regular, driveable lanes</summary>
        [JsonProperty("lane_number", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [Range(1, int.MaxValue)]
        public int? LaneNumber { get; set; }

        /// <summary>A list of restrictions specific to the lane</summary>
        [JsonProperty("restrictions", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<LaneRestriction> Restrictions { get; set; }

        private IDictionary<string, object> _additionalProperties = new Dictionary<string, object>();

        [JsonExtensionData]
        public IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties; }
            set { _additionalProperties = value; }
        }


    }
}