using System.Runtime.Serialization;

namespace Wzdx.v4.Devices
{
    /// <summary>
    /// Options for what a FlashingBeacon is being used to indicate
    /// </summary>
    public enum FlashingBeaconFunction
    {
        [EnumMember(Value = @"vehicle-entering")]
        VehicleEntering = 1,
        [EnumMember(Value = @"queue-warning")]
        QueueWarning = 2,
        [EnumMember(Value = @"reduced-speed")]
        ReducedSpeed = 3,
        [EnumMember(Value = @"workers-present")]
        WorkersPresent = 4
    }
}