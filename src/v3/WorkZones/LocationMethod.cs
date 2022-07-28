using System.Runtime.Serialization;

namespace Wzdx.v3.WorkZones
{
    /// <summary>
    /// The typical method used to locate the beginning and end of a work zone impact area
    /// </summary>
    public enum LocationMethod
    {
        [EnumMember(Value = @"unknown")]
        Unknown = 1,
        [EnumMember(Value = @"channel-device-method")]
        ChannelDeviceMethod = 2,
        [EnumMember(Value = @"sign-method")]
        SignMethod = 3,
        [EnumMember(Value = @"junction-method")]
        JunctionMethod = 4,
        [EnumMember(Value = @"other")]
        Other = 5
    }
}