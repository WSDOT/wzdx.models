using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Wsdot.Wzdx.v4.Devices
{
    /// <summary>
    /// A traffic sensor deployed on a roadway which captures traffic metrics (e.g. speed, volume, occupancy) over a collection interval
    /// </summary>
    public class TrafficSensor : IFieldDevice
    {
        [JsonProperty("core_details", Required = Required.Always)]
        [Required]
        public FieldDeviceCoreDetails CoreDetails { get; set; } = new FieldDeviceCoreDetails();

        /// <summary>
        /// The UTC date and time where the TrafficSensor data collection started. The averages and totals contained in the TrafficSensor data apply to the inclusive interval of 'collection_interval_start_date' to 'collection_interval_end_date'
        /// </summary>
        [JsonProperty("collection_interval_start_date", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public System.DateTimeOffset CollectionIntervalStartDate { get; set; }

        /// <summary>
        /// The UTC date and time where the TrafficSensor data collection ended. The averages and totals contained in the TrafficSensor data apply to the inclusive interval of 'collection_interval_start_date' to 'collection_interval_end_date'
        /// </summary>
        [JsonProperty("collection_interval_end_date", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public System.DateTimeOffset CollectionIntervalEndDate { get; set; }

        /// <summary>
        /// The average speed of vehicles across all lanes over the collection interval in kilometers per hour
        /// </summary>
        [JsonProperty("average_speed_kph", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [Range(0, int.MaxValue)]
        public int AverageSpeedKph { get; set; }

        /// <summary>
        /// The rate of vehicles passing by the sensor during the collection interval in vehicles per hour
        /// </summary>
        [JsonProperty("volume_vph", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [Range(0, int.MaxValue)]
        public int VolumeVph { get; set; }

        /// <summary>
        /// The percent of time the roadway section monitored by the sensor was occupied by a vehicle over the collection interval
        /// </summary>
        [JsonProperty("occupancy_percent", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [Range(0, int.MaxValue)]
        public int OccupancyPercent { get; set; }

        [JsonProperty("lane_data", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<TrafficSensorLaneData> LaneData { get; set; } = new HashSet<TrafficSensorLaneData>();


    }
}