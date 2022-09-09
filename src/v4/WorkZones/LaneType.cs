using System;
using System.Runtime.Serialization;

namespace Wzdx.v4.WorkZones
{
    /// <summary>
    /// An indication of the type of lane or shoulder
    /// </summary>
    public enum LaneType
    {
        [EnumMember(Value = @"general")]
        General = 31,
        [EnumMember(Value = @"exit-lane")]
        ExitLane = 35,
        [EnumMember(Value = @"exit-ramp")]
        ExitRamp = 36,
        [EnumMember(Value = @"entrance-lane")]
        EntranceLane = 33,
        [EnumMember(Value = @"entrance-ramp")]
        EntranceRamp = 34,
        [EnumMember(Value = @"sidewalk")]
        Sidewalk = 8,
        [EnumMember(Value = @"bike-lane")]
        BikeLane = 9,
        [EnumMember(Value = @"shoulder")]
        Shoulder = 11,
        [EnumMember(Value = @"parking")]
        Parking = 37,
        [EnumMember(Value = @"median")]
        Median = 32,
        [Obsolete("Depreciated, use the new `two-way-center-turn-lane` type")]
        [EnumMember(Value = @"center-left-turn-lane")]
        CenterLeftTurnLane = 14,
        [EnumMember(Value = @"two-way-center-turn-lane")]
        TwoWayCenterTurnLane = 38 
    }
}