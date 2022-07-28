using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wzdx.v4.Devices
{
    /// <summary>
    /// Describes a specific location where a LocationMarker is placed, such as the start or end of a work zone road event
    /// </summary>

    public class MarkedLocation
    {
        [JsonProperty("type", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public MarkedLocationType Type { get; set; }

        /// <summary>
        /// The ID of a RoadEventFeature that the MarkedLocation applies to
        /// </summary>
        [JsonProperty("road_event_id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string RoadEventId { get; set; }
    }
}