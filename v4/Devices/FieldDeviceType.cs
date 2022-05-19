using System.Runtime.Serialization;

namespace Wsdot.Wzdx.v4.Devices
{
    /// <summary>
    /// The type of field device
    /// </summary>
    public enum FieldDeviceType
    {
        [EnumMember(Value = @"arrow-board")]
        ArrowBoard = 1,
        [EnumMember(Value = @"camera")]
        Camera = 2,
        [EnumMember(Value = @"dynamic-message-sign")]
        DynamicMessageSign = 3,
        [EnumMember(Value = @"flashing-beacon")]
        FlashingBeacon = 4,
        [EnumMember(Value = @"hybrid-sign")]
        HybridSign = 5,
        [EnumMember(Value = @"location-marker")]
        LocationMarker = 6,
        [EnumMember(Value = @"traffic-sensor")]
        TrafficSensor = 7
    }
}