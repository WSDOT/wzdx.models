using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wsdot.Wzdx.v4.WorkZones
{
    /// <summary>
    /// A road event describing a section of roadway and the limitations of how that section can be used
    /// </summary>
    public class RestrictionRoadEvent : RoadEvent
    {
        public RestrictionRoadEvent()
        {
            CoreDetails.EventType = EventType.Restriction;
        }

        /// <summary>
        /// A list of zero or more restrictions applying to the road event
        /// </summary>
        [JsonProperty("restrictions", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<Restriction> Restrictions { get; set; } = new HashSet<Restriction>();

        /// <summary>
        /// A list of individual lanes within a road event (roadway segment)
        /// </summary>
        [JsonProperty("lanes", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<Lane> Lanes { get; set; } = new HashSet<Lane>();
    }
}