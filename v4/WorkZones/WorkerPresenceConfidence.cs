using System.Runtime.Serialization;

namespace Wsdot.Wzdx.v4.WorkZones
{
    /// <summary>
    /// A high-level description of the feed publisher's confidence in the reported WorkerPresence value of are_workers_present
    /// </summary>
    public enum WorkerPresenceConfidence
    {
        [EnumMember(Value = @"low")]
        Low = 1,
        [EnumMember(Value = @"medium")]
        Medium = 2,
        [EnumMember(Value = @"high")]
        High = 3
    }
}