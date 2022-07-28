using System.Runtime.Serialization;

namespace Wzdx.v4.WorkZones
{
    /// <summary>
    /// The type of vehicle restriction on a roadway
    /// </summary>
    public enum RestrictionType
    {
        [EnumMember(Value = @"no-trucks")]
        NoTrucks = 1,
        [EnumMember(Value = @"travel-peak-hours-only")]
        TravelPeakHoursOnly = 2,
        [EnumMember(Value = @"hov-3")]
        Hov3 = 3,
        [EnumMember(Value = @"hov-2")]
        Hov2 = 4,
        [EnumMember(Value = @"no-parking")]
        NoParking = 5,
        [EnumMember(Value = @"reduced-width")]
        ReducedWidth = 6,
        [EnumMember(Value = @"reduced-height")]
        ReducedHeight = 7,
        [EnumMember(Value = @"reduced-length")]
        ReducedLength = 8,
        [EnumMember(Value = @"reduced-weight")]
        ReducedWeight = 9,
        [EnumMember(Value = @"axle-load-limit")]
        AxleLoadLimit = 10,
        [EnumMember(Value = @"gross-weight-limit")]
        GrossWeightLimit = 11,
        [EnumMember(Value = @"towing-prohibited")]
        TowingProhibited = 12,
        [EnumMember(Value = @"permitted-oversize-loads-prohibited")]
        PermittedOversizeLoadsProhibited = 13,
        [EnumMember(Value = @"local-access-only")]
        LocalAccessOnly = 14
    }
}