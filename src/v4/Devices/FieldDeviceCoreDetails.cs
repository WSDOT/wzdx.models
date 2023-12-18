using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wzdx.v4.Devices
{
    /// <summary>
    /// The core details both configuration and current state of a field device that are shared by all types of field devices
    /// </summary>
    public class FieldDeviceCoreDetails
    {
        [JsonProperty("device_type", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public FieldDeviceType DeviceType { get; set; }

        /// <summary>
        /// Identifies the data source from which the field device information is sourced from
        /// </summary>
        [JsonProperty("data_source_id", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string DataSourceId { get; set; }

        /// <summary>
        /// A list of publicly known names of the road on which the field device is located. This may include the road number designated by a jurisdiction such as a county, state or interstate (e.g. I-5, VT 133)
        /// </summary>
        [JsonProperty("road_names", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
        [Required]
        [MinLength(1)]
        public ICollection<string> RoadNames { get; set; } 

        [JsonProperty("device_status", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public FieldDeviceStatus DeviceStatus { get; set; }

        /// <summary>
        /// The UTC date and time (formatted according to RFC 3339, Section 5.6) when the field device data was last updated (e.g. 2020-11-03T19:37:00Z)
        /// </summary>
        [JsonProperty("update_date", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public System.DateTimeOffset UpdateDate { get; set; }

        /// <summary>
        /// A yes/no value indicating if the field device location (parent FieldDeviceFeature's geometry) is determined automatically from an onboard GPS (true) or manually set/overridden (false)
        /// </summary>
        [JsonProperty("has_automatic_location", Required = Required.Always)]
        public bool HasAutomaticLocation { get; set; }

        /// <summary>
        /// A human-readable name for the field device
        /// </summary>
        [JsonProperty("name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// A description of the field device.
        /// </summary>
        [JsonProperty("description", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        /// <summary>
        /// A list of messages associated with the device's status, if applicable. Used to provide additional information about the status such as specific warning or error message.
        /// </summary>
        [JsonProperty("status_messages", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<string> StatusMessages { get; set; }

        /// <summary>
        /// A list of one or more IDs of a RoadEventFeatures that the device is associated with
        /// </summary>
        [JsonProperty("road_event_ids", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<string> RoadEventIds { get; set; }

        /// <summary>
        /// The linear distance measured against a milepost marker along a roadway where the device is located
        /// </summary>
        [JsonProperty("milepost", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double? Milepost { get; set; }

        /// <summary>
        /// The make or manufacturer of the device
        /// </summary>
        [JsonProperty("make", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Make { get; set; }

        /// <summary>
        /// The model of the device
        /// </summary>
        [JsonProperty("model", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Model { get; set; }

        /// <summary>
        /// The serial number of the device
        /// </summary>
        [JsonProperty("serial_number", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string SerialNumber { get; set; }

        /// <summary>
        /// The version of firmware the device is using to operate
        /// </summary>
        [JsonProperty("firmware_version", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string FirmwareVersion { get; set; }

        /// <summary>
        /// A yes/no value indicating if the arrow board is actively moving (not statically placed) as part of a mobile work zone operation.
        /// </summary>
        [JsonProperty("is_moving", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsMoving { get; set; }

		/// <summary>
		/// The velocity of the device in kilometers per hour.
		/// </summary>
		[JsonProperty("velocity_kph", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
		public int? VelocityKph { get; set; }

        [JsonProperty("road_direction", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Direction Direction { get; set; } = Direction.Undefined;
    }
}