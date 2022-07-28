using System.Runtime.Serialization;

namespace Wzdx.v4.Devices
{
    /// <summary>
    /// Options for the function of the dynamic message displayed by the electronic display on a HybridSign
    /// </summary>
    public enum HybridSignDynamicMessageFunction
    {
        [EnumMember(Value = @"other")]
        Other = 1,
        [EnumMember(Value = @"speed-limit")]
        SpeedLimit = 2,
        [EnumMember(Value = @"travel-time")]
        TravelTime = 3
    }
}