using System.Runtime.Serialization;

namespace Wsdot.Wzdx.v3.WorkZones
{
    /// <summary>The impact to vehicular lanes along a single road in a single direction</summary>

    public enum VehicleImpact
    {
        [EnumMember(Value = @"unknown")]
        Unknown = 1,
        [EnumMember(Value = @"all-lanes-closed")]
        AllLanesClosed = 2,
        [EnumMember(Value = @"some-lanes-closed")]
        SomeLanesClosed = 3,
        [EnumMember(Value = @"all-lanes-open")]
        AllLanesOpen = 4,
        [EnumMember(Value = @"alternating-one-way")]
        AlternatingOneWay = 5
    }
}