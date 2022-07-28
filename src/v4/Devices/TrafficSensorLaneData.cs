using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Wzdx.v4.Devices
{
    /// <summary>
    /// data for a single lane within a RoadEvent measured by a TrafficSensor deployed on the roadway
    /// </summary>
    public class TrafficSensorLaneData
    {
        /// <summary>
        /// The ID of a RoadEventFeature that the measured lane occurs in
        /// </summary>
        [JsonProperty("road_event_id", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string RoadEventId { get; set; }

        /// <summary>
        /// The lane's position in sequence within the road event specified by the 'road_event_id' property
        /// </summary>
        [JsonProperty("lane_order", Required = Required.Always)]
        [Range(1, int.MaxValue)]
        public int LaneOrder { get; set; }

        /// <summary>
        /// The average speed of traffic in the lane over the collection interval (in kilometers per hour)
        /// </summary>
        [JsonProperty("average_speed_kph", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [Range(1, int.MaxValue)]
        public int? AverageSpeedKph { get; set; }

        /// <summary>
        /// The rate of vehicles passing by the sensor in the lane during the collection interval (in vehicles per hour)
        /// </summary>
        [JsonProperty("volume_vph", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [Range(0, int.MaxValue)]
        public int? VolumeVph { get; set; }

        /// <summary>
        /// The percent of time the lane monitored by the sensor was occupied by a vehicle over the collection interval
        /// </summary>
        [JsonProperty("occupancy_percent", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [Range(0, int.MaxValue)]
        public int? OccupancyPercent { get; set; }
    }
}