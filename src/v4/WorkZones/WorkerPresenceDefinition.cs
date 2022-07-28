using System.Runtime.Serialization;

namespace Wzdx.v4.WorkZones
{
    /// <summary>
    /// Situations in which workers may be considered present in a work zone
    /// </summary>
    public enum WorkerPresenceDefinition
    {
        [EnumMember(Value = @"workers-in-work-zone-working")]
        WorkersInWorkZoneWorking = 1,
        [EnumMember(Value = @"workers-in-work-zone-not-working")]
        WorkersInWorkZoneNotWorking = 2,
        [EnumMember(Value = @"mobile-equipment-in-work-zone-moving")]
        MobileEquipmentInWorkZoneMoving = 3,
        [EnumMember(Value = @"mobile-equipment-in-work-zone-not-working")]
        MobileEquipmentInWorkZoneNotWorking = 4,
        [EnumMember(Value = @"fixed-equipment-in-work-zone")]
        FixedEquipmentInWorkZone = 5,
        [EnumMember(Value = @"humans-behind-barrier")]
        HumansBehindBarrier = 6,
        [EnumMember(Value = @"humans-in-right-of-way")]
        HumansInRightOfWay = 7
    }
}