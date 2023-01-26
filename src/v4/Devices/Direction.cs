using System.Runtime.Serialization;

namespace Wzdx.v4.Devices
{
    /// <summary>
    /// The direction for a road event based on standard naming for US roads; indicates the direction the traffic flow regardless of the real heading angle
    /// </summary>
    public enum Direction
    {
        [EnumMember(Value = @"northbound")]
        Northbound = 1,
        [EnumMember(Value = @"eastbound")]
        Eastbound = 2,
        [EnumMember(Value = @"southbound")]
        Southbound = 3,
        [EnumMember(Value = @"westbound")]
        Westbound = 4,
        [EnumMember(Value = @"undefined")]
        Undefined = 5,
        [EnumMember(Value = @"unknown")]
        Unknown = 6
    }
}