using System.Runtime.Serialization;

namespace Wzdx.v4.Devices
{
    /// <summary>
    /// a list of options for the posted pattern on an ArrowBoard
    /// </summary>
    public enum ArrowBoardPattern
    {
        [EnumMember(Value = @"bidirectional-arrow-flashing")]
        BidirectionalArrowFlashing = 2,
        [EnumMember(Value = @"bidirectional-arrow-static")]
        BidirectionalArrowStatic = 3,
        [EnumMember(Value = @"blank")]
        Blank = 1,
        [EnumMember(Value = @"diamonds-alternating")]
        DiamondsAlternating = 4,
        [EnumMember(Value = @"four-corners-flashing")]
        FourCornersFlashing = 5,
        [EnumMember(Value = @"left-arrow-flashing")]
        LeftArrowFlashing = 6,
        [EnumMember(Value = @"left-arrow-sequential")]
        LeftArrowSequential = 7,
        [EnumMember(Value = @"left-arrow-static")]
        LeftArrowStatic = 8,
        [EnumMember(Value = @"left-chevron-flashing")]
        LeftChevronFlashing = 9,
        [EnumMember(Value = @"left-chevron-sequential")]
        LeftChevronSequential = 10,
        [EnumMember(Value = @"left-chevron-static")]
        LeftChevronStatic = 11,
        [EnumMember(Value = @"line-flashing")]
        LineFlashing = 12,
        [EnumMember(Value = @"right-arrow-flashing")]
        RightArrowFlashing = 13,
        [EnumMember(Value = @"right-arrow-sequential")]
        RightArrowSequential = 14,
        [EnumMember(Value = @"right-arrow-static")]
        RightArrowStatic = 15,
        [EnumMember(Value = @"right-chevrons-flashing")]
        RightChevronsFlashing = 16,
        [EnumMember(Value = @"right-chevrons-sequential")]
        RightChevronsSequential = 17,
        [EnumMember(Value = @"right-chevrons-static")]
        RightChevronsStatic = 18,
        [EnumMember(Value = @"unknown")]
        Unknown = 0
    }
}