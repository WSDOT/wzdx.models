using System.Runtime.Serialization;

namespace Wsdot.Wzdx.v4.Devices
{
    /// <summary>
    /// Options for what a MarkedLocation can mark, such as the start or end of a road event
    /// </summary>
    public enum MarkedLocationType
    {
        [EnumMember(Value = @"afad")]
        Afad = 1,
        [EnumMember(Value = @"flagger")]
        Flagger = 2,
        [EnumMember(Value = @"lane-shift")]
        LaneShift = 3,
        [EnumMember(Value = @"lane-closure")]
        LaneClosure = 4,
        [EnumMember(Value = @"temporary-traffic-signal")]
        TemporaryTrafficSignal = 5,
        [EnumMember(Value = @"road-event-start")]
        RoadEventStart = 6,
        [EnumMember(Value = @"road-event-end")]
        RoadEventEnd = 7,
        [EnumMember(Value = @"work-zone-start")]
        WorkZoneStart = 8,
        [EnumMember(Value = @"work-zone-end")]
        WorkZoneEnd = 9
    }
}