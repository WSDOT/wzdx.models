using System.Runtime.Serialization;

namespace Wsdot.Wzdx.v3.WorkZones
{
    /// <summary>Units of measure used for the lane restriction value</summary>

    public enum LaneRestrictionUnit
    {
        [EnumMember(Value = @"feet")]
        Feet = 1,
        [EnumMember(Value = @"inches")]
        Inches = 2,
        [EnumMember(Value = @"centimeters")]
        Centimeters = 3,
        [EnumMember(Value = @"pounds")]
        Pounds = 4,
        [EnumMember(Value = @"tons")]
        Tons = 5,
        [EnumMember(Value = @"kilograms")]
        Kilograms = 6
    }
}