using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Wsdot.Wzdx.v4.WorkZones
{
    /// <summary>
    /// Identifies both sequential and hierarchical relationships between road events and other entities. For example, a relationship can be used to link multiple road events to a common 'parent', such as a project or phase, or identify a sequence of road events
    /// </summary>
    public class Relationship
    {
        /// <summary>
        /// Indicates the first (can be multiple) road event in a sequence of road events by RoadEventFeature 'id'
        /// </summary>
        [JsonProperty("first", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [MinLength(1)]
        public ICollection<string> First { get; set; }

        /// <summary>
        /// Indicates the next (can be multiple) road event in a sequence of road events by RoadEventFeature 'id'
        /// </summary>
        [JsonProperty("next", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [MinLength(1)]
        public ICollection<string> Next { get; set; }

        /// <summary>
        /// Indicates entities that the road event with this relationship is a part of, such as a work zone project or phase. Values can but do not have to correspond to a WZDx entity
        /// </summary>
        [JsonProperty("parents", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [MinLength(1)]
        public ICollection<string> Parents { get; set; }

        /// <summary>
        /// Indicates entities that are part of the road event with this relationship, such as a detour or piece of equipment. Values can but do not have to correspond to a WZDx entity
        /// </summary>
        [JsonProperty("children", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [MinLength(1)]
        public ICollection<string> Children { get; set; }
    }
}