using System.Runtime.Serialization;

namespace Wsdot.Wzdx.v4.WorkZones
{
    /// <summary>
    /// The impact to vehicular lanes along a single road in a single direction
    /// </summary>
    public enum VehicleImpact
    {
        [EnumMember(Value = @"unknown")]
        Unknown = 1,
        [EnumMember(Value = @"all-lanes-closed")]
        AllLanesClosed = 2,
        [EnumMember(Value = @"some-lanes-closed")]
        SomeLanesClosed = 3,
        [EnumMember(Value = @"all-lanes-open")]
        AllLanesOpen = 4,
        [EnumMember(Value = @"alternating-one-way")]
        AlternatingOneWay = 5,
        [EnumMember(Value = @"some-lanes-closed-merge-left")]
        SomeLanesClosedMergeLeft = 6,
        [EnumMember(Value = @"some-lanes-closed-merge-right")]
        SomeLanesClosedMergeRight = 7,
        [EnumMember(Value = @"all-lanes-open-shift-left")]
        AllLanesOpenShiftLeft = 8,
        [EnumMember(Value = @"all-lanes-open-shift-right")]
        AllLanesOpenShiftRight = 9,
        [EnumMember(Value = @"some-lanes-closed-split")]
        SomeLanesClosedSplit = 10,
        [EnumMember(Value = @"flagging")]
        Flagging = 11,
        [EnumMember(Value = @"temporary-traffic-signal")]
        TemporaryTrafficSignal = 12
    }
}