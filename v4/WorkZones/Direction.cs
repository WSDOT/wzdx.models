using System.Runtime.Serialization;

namespace Wsdot.Wzdx.v4.WorkZones
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
        Westbound = 4
    }
}