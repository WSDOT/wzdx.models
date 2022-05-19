using System.Runtime.Serialization;

namespace Wsdot.Wzdx.v4.Devices
{
    /// <summary>
    /// The operational status of a field device
    /// </summary>
    public enum FieldDeviceStatus
    {
        [EnumMember(Value = @"unknown")]
        Unknown = 1,
        [EnumMember(Value = @"ok")]
        Ok = 2,
        [EnumMember(Value = @"warning")]
        Warning = 3,
        [EnumMember(Value = @"error")]
        Error = 4
    }
}