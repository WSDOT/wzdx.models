using System.Runtime.Serialization;

namespace Wsdot.Wzdx.v3.WorkZones
{
    /// <summary>An indication of how a geographical coordinate was defined</summary>

    public enum SpatialVerification
    {
        [EnumMember(Value = @"estimated")]
        Estimated = 1,
        [EnumMember(Value = @"verified")]
        Verified = 2
    }
}