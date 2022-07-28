using System.Runtime.Serialization;

namespace Wzdx.v4.WorkZones
{
    /// <summary>
    /// The status of the lane for the traveling public
    /// </summary>
    public enum LaneStatus
    {
        [EnumMember(Value = @"open")]
        Open = 1,
        [EnumMember(Value = @"closed")]
        Closed = 2,
        [EnumMember(Value = @"shift-left")]
        ShiftLeft = 3,
        [EnumMember(Value = @"shift-right")]
        ShiftRight = 4,
        [EnumMember(Value = @"merge-left")]
        MergeLeft = 5,
        [EnumMember(Value = @"merge-right")]
        MergeRight = 6,
        [EnumMember(Value = @"alternating-flow")]
        AlternatingFlow = 8
    }
}