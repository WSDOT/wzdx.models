using System.Runtime.Serialization;

namespace Wsdot.Wzdx.v3.WorkZones
{
    /// <summary>The status of the road event</summary>

    public enum EventStatus
    {
        [EnumMember(Value = @"planned")]
        Planned = 1,
        [EnumMember(Value = @"pending")]
        Pending = 2,
        [EnumMember(Value = @"active")]
        Active = 3,
        [EnumMember(Value = @"completed")]
        Completed = 4,
        [EnumMember(Value = @"cancelled")]
        Cancelled = 5
    }
}