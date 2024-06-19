using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wzdx.v4.Devices
{
    /// <summary>
    /// Describes a temporary traffic signal deployed on a roadway
    /// </summary>
    public class TrafficSignal : IFieldDevice
    {
        [JsonProperty("core_details", Required = Required.Always)]
        [Required]
        public FieldDeviceCoreDetails CoreDetails { get; set; } = new FieldDeviceCoreDetails()
            {
                DeviceType = FieldDeviceType.TrafficSignal,
                DeviceStatus = FieldDeviceStatus.Unknown
            };

        [JsonProperty("mode", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public TrafficSignalMode Mode { get; set; } = TrafficSignalMode.Unknown;
    }
}