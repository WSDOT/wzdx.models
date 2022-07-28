using System.Runtime.Serialization;

namespace Wzdx.v3.WorkZones
{
    /// <summary>An indication of the type of lane or shoulder</summary>

    public enum LaneType
    {
        [EnumMember(Value = @"lane")]
        Lane = 1,
        [EnumMember(Value = @"right-turning-lane")]
        RightTurningLane = 2,
        [EnumMember(Value = @"left-turning-lane")]
        LeftTurningLane = 3,
        [EnumMember(Value = @"right-exit-lane")]
        RightExitLane = 4,
        [EnumMember(Value = @"left-exit-lane")]
        LeftExitLane = 5,
        [EnumMember(Value = @"right-entrance-lane")]
        RightEntranceLane = 6,
        [EnumMember(Value = @"left-entrance-lane")]
        LeftEntranceLane = 7,
        [EnumMember(Value = @"sidewalk")]
        Sidewalk = 8,
        [EnumMember(Value = @"bike-lane")]
        BikeLane = 9,
        [EnumMember(Value = @"alternating-flow-lane")]
        AlternatingFlowLane = 10,
        [EnumMember(Value = @"shoulder")]
        Shoulder = 11,
        [EnumMember(Value = @"hov-lane")]
        HovLane = 12,
        [EnumMember(Value = @"reversible-lane")]
        ReversibleLane = 13,
        [EnumMember(Value = @"center-left-turn-lane")]
        CenterLeftTurnLane = 14,
        [EnumMember(Value = @"left-lane")]
        LeftLane = 15,
        [EnumMember(Value = @"right-lane")]
        RightLane = 16,
        [EnumMember(Value = @"middle-lane")]
        MiddleLane = 17,
        [EnumMember(Value = @"center-lane")]
        CenterLane = 18,
        [EnumMember(Value = @"right-shoulder")]
        RightShoulder = 19,
        [EnumMember(Value = @"left-shoulder")]
        LeftShoulder = 20,
        [EnumMember(Value = @"right-merging-lane")]
        RightMergingLane = 21,
        [EnumMember(Value = @"left-merging-lane")]
        LeftMergingLane = 22,
        [EnumMember(Value = @"right-exit-ramp")]
        RightExitRamp = 23,
        [EnumMember(Value = @"right-second-exit-ramp")]
        RightSecondExitRamp = 24,
        [EnumMember(Value = @"left-exit-ramp")]
        LeftExitRamp = 25,
        [EnumMember(Value = @"left-second-exit-ramp")]
        LeftSecondExitRamp = 26,
        [EnumMember(Value = @"right-entrance-ramp")]
        RightEntranceRamp = 27,
        [EnumMember(Value = @"right-second-entrance-ramp")]
        RightSecondEntranceRamp = 28,
        [EnumMember(Value = @"left-entrance-ramp")]
        LeftEntranceRamp = 28,
        [EnumMember(Value = @"left-second-entrance-ramp")]
        LeftSecondEntranceRamp = 30
    }
}