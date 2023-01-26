using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wzdx.v4.Devices
{
    /// <summary>
    /// An electronic, connected arrow board which can display an arrow pattern to direct traffic
    /// </summary>

    public class ArrowBoard : IFieldDevice
    {
        [JsonProperty("core_details", Required = Required.Always)]
        [Required]
        public FieldDeviceCoreDetails CoreDetails { get; set; } = new FieldDeviceCoreDetails()
            {
                DeviceType = FieldDeviceType.ArrowBoard,
                DeviceStatus = FieldDeviceStatus.Unknown
            };

        [JsonProperty("pattern", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public ArrowBoardPattern Pattern { get; set; }

        /// <summary>
        /// A yes/no value indicating if the arrow board is actively moving (not statically placed) as part of a mobile work zone operation.
        /// </summary>
        [Obsolete("Use core_details is_moving property.")]
        [JsonProperty("is_moving", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsMoving { get; set; }

        /// <summary>
        /// A yes/no value indicating if the arrow board is in the stowed/transport position (true) or deployed/upright position (false)
        /// </summary>
        [JsonProperty("is_in_transport_position", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsInTransportPosition { get; set; }
    }
}