using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wzdx.v4.Devices
{
    /// <summary>
    /// A flashing beacon light of any form (e.g. trailer-mounted, vehicle), used to indicate something and capture driver attention
    /// </summary>
    public class FlashingBeacon : IFieldDevice
    {
        [JsonProperty("core_details", Required = Required.Always)]
        [Required]
        public FieldDeviceCoreDetails CoreDetails { get; set; } = new FieldDeviceCoreDetails()
        {
            DeviceType = FieldDeviceType.FlashingBeacon,
            DeviceStatus = FieldDeviceStatus.Unknown
        };

        /// <summary>
        /// The text on the sign the beacon is mounted on, if applicable
        /// </summary>
        [JsonProperty("sign_text", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string SignText { get; set; }

        [JsonProperty("function", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public FlashingBeaconFunction Function { get; set; }

        /// <summary>
        /// A yes/no value indicating if the flashing beacon is currently in use and flashing
        /// </summary>
        [JsonProperty("is_flashing", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsFlashing { get; set; }
    }
}