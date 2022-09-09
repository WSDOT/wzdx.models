using System.Runtime.Serialization;

namespace Wzdx.v4.Devices
{
    /// <summary>
    /// Describes the current operating mode of a TrafficSignal
    /// </summary>
    public enum TrafficSignalMode
    {
        [EnumMember(Value = "unknown")]
        Unknown = 0,
        [EnumMember(Value = "blank")]
        Blank = 1,
        [EnumMember(Value = "manual")]
        Manual = 2,
        [EnumMember(Value = "flashing-red")]
        FlashingRed = 3,
        [EnumMember(Value = "flashing-yellow")]
        FlashingYellow = 4,
        [EnumMember(Value = "fully-actuated")]
        FullyActuated = 5,
        [EnumMember(Value = "pre-timed")]
        PreTimed = 6,
        [EnumMember(Value = "semi-actuated")]
        SemiActuated = 7
    }
}