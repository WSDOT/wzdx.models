using System.Runtime.Serialization;

namespace Wzdx.v4.WorkZones
{
    /// <summary>
    /// Unit of measurement, used when providing a unit to accompany a value
    /// </summary>
    public enum UnitOfMeasurement
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