using System.Runtime.Serialization;

namespace Wsdot.Wzdx.v3.WorkZones
{
    /// <summary>A measure of how accurate a date-time is</summary>

    public enum TimeVerification
    {
        [EnumMember(Value = @"estimated")]
        Estimated = 1,
        [EnumMember(Value = @"verified")]
        Verified = 2
    }
}