using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wzdx.v4.Devices
{
    /// <summary>
    /// A hybrid sign that contains static text (e.g. on an aluminum sign) along with a single electronic message display, used to provide information to travelers
    /// </summary>
    public class HybridSign : IFieldDevice
    {
        [JsonProperty("core_details", Required = Required.Always)]
        [Required]
        public FieldDeviceCoreDetails CoreDetails { get; set; } = new FieldDeviceCoreDetails()
        {
            DeviceType = FieldDeviceType.HybridSign,
            DeviceStatus = FieldDeviceStatus.Unknown
        };

        [JsonProperty("dynamic_message_function", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public HybridSignDynamicMessageFunction DynamicMessageFunction { get; set; }

        /// <summary>
        /// A text representation of the message currently posted to the dynamic electronic component of the hybrid sign
        /// </summary>
        [JsonProperty("dynamic_message_text", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string DynamicMessageText { get; set; }

        /// <summary>
        /// The static text on the non-electronic component of the hybrid sign
        /// </summary>
        [JsonProperty("static_sign_text", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string StaticSignText { get; set; }
    }
}