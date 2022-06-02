using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Wsdot.Wzdx.v4.Devices
{
    /// <summary>
    /// A camera device deployed in the field, capable of capturing still images
    /// </summary>
    public class Camera : IFieldDevice
    {
        [JsonProperty("core_details", Required = Required.Always)]
        [Required]
        public FieldDeviceCoreDetails CoreDetails { get; set; } = new FieldDeviceCoreDetails()
            {
                DeviceType = FieldDeviceType.Camera,
                DeviceStatus = FieldDeviceStatus.Unknown
            };

        /// <summary>
        /// A URL pointing to an image file for the camera image still
        /// </summary>
        [JsonProperty("image_url", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Uri ImageUrl { get; set; }

        /// <summary>
        /// The UTC date and time when the image was captured
        /// </summary>
        [JsonProperty("image_timestamp", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset ImageTimestamp { get; set; }
    }
}