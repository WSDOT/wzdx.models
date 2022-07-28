using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Wzdx.v4.Devices
{
    /// <summary>
    /// An electronic traffic sign deployed on the roadway, used to provide information to travelers
    /// </summary>
    public class DynamicMessageSign : IFieldDevice
    {
        [JsonProperty("core_details", Required = Required.Always)]
        [Required]
        public FieldDeviceCoreDetails CoreDetails { get; set; } = new FieldDeviceCoreDetails()
        {
            DeviceType = FieldDeviceType.DynamicMessageSign,
            DeviceStatus = FieldDeviceStatus.Unknown
        };

        /// <summary>
        /// A MULTI-formatted string describing the message currently posted to the sign
        /// </summary>
        [JsonProperty("message_multi_string", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string MessageMultiString { get; set; }
    }
}