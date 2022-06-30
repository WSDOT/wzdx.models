using System.Runtime.Serialization;

namespace Wsdot.Wzdx.v3.WorkZones
{
    /// <summary>A high-level text description of the type of work being done in a road event</summary>

    public enum WorkTypeName
    {
        [EnumMember(Value = @"maintenance")]
        Maintenance = 1,
        [EnumMember(Value = @"minor-road-defect-repair")]
        MinorRoadDefectRepair = 2,
        [EnumMember(Value = @"roadside-work")]
        RoadsideWork = 3,
        [EnumMember(Value = @"overhead-work")]
        OverheadWork = 4,
        [EnumMember(Value = @"below-road-work")]
        BelowRoadWork = 5,
        [EnumMember(Value = @"barrier-work")]
        BarrierWork = 6,
        [EnumMember(Value = @"surface-work")]
        SurfaceWork = 7,
        [EnumMember(Value = @"painting")]
        Painting = 8,
        [EnumMember(Value = @"roadway-relocation")]
        RoadwayRelocation = 9,
        [EnumMember(Value = @"roadway-creation")]
        RoadwayCreation = 10
    }
}