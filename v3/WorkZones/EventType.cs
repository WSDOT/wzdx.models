using System.Runtime.Serialization;

namespace Wsdot.Wzdx.v3.WorkZones
{
    /// <summary>The type of WZDx road event</summary>

    public enum EventType
    {
        [EnumMember(Value = @"work-zone")]
        WorkZone = 1,
        [EnumMember(Value = @"detour")]
        Detour = 2
    }
}