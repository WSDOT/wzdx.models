using System.Runtime.Serialization;

namespace Wsdot.Wzdx.v4.WorkZones
{
    /// <summary>
    /// Describes methods for how worker presence in a work zone event area is determined
    /// </summary>
    public enum WorkerPresenceMethod
    {
        [EnumMember(Value = @"camera-monitoring")]
        CameraMonitoring = 0,
        [EnumMember(Value = @"arrow-board-present")]
        ArrowBoardPresent = 1,
        [EnumMember(Value = @"cones-present")]
        ConesPresent = 2,
        [EnumMember(Value = @"maintenance-vehicle-present")]
        MaintenanceVehiclePresent = 3,
        [EnumMember(Value = @"wearables-present")]
        WearablesPresent = 4,
        [EnumMember(Value = @"mobile-device-present")]
        MobileDevicePresent = 5,
        [EnumMember(Value = @"check-in-app")]
        CheckInApp = 6,
        [EnumMember(Value = @"check-in-verbal")]
        CheckInVerbal = 7,
        [EnumMember(Value = @"scheduled")]
        Scheduled = 8
    }
}