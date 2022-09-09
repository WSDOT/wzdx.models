using System.Runtime.Serialization;

namespace Wzdx.v4.WorkZones
{
    /// <summary>
    /// Describes how a road event is related to the road event that the RelatedRoadEvent object occurs on
    /// </summary>
    public enum RelatedRoadEventType
    {
        [EnumMember(Value = "first-in-sequence")]
        FirstInSequence = 5,
        [EnumMember(Value = "next-in-sequence")]
        NextInSequence = 6,
        [EnumMember(Value = "first-occurrence")]
        FirstOccurrence = 7,
        [EnumMember(Value = "next-occurrence")]
        NextOccurrence = 8,
        [EnumMember(Value = "related-work-zone")]
        RelatedWorkZone = 9,
        [EnumMember(Value = "related-detour")]
        RelatedDetour = 10,
        [EnumMember(Value = "planned-moving-operation")]
        PlannedMovingOperation = 11,
        [EnumMember(Value = "active-moving-operation")]
        ActiveMovingOperation = 12
    }
}