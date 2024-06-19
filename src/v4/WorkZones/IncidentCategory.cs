using System.Runtime.Serialization;

namespace Wzdx.v4.WorkZones
{
    public enum IncidentCategory
    {
        //Vehicle or other crash types disrupting expected transportation operations.
        [EnumMember(Value = "crash")]
        Crash,
        [EnumMember(Value = "wind")]    //Winds disrupting expected transportation operations.
        Wind,
        [EnumMember(Value = "disaster")]    //Natural or man-made catastrophes disrupting expected transportation operations.
        Disaster,
        [EnumMember(Value = "special-event")]	//Cultural or societal happenings disrupting expected transportation operations.
        SpecialEvent
    }
}