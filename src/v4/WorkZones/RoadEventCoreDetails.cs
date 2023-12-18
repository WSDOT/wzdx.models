using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wzdx.v4.WorkZones
{
    /// <summary>
    /// The core details of an event occurring on a roadway (i.e. a road event) that is shared by all types of road events
    /// </summary>
    public class RoadEventCoreDetails
    {
        /// <summary>
        /// Identifies the data source from which the road event data is sourced from
        /// </summary>
        [JsonProperty("data_source_id", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string DataSourceId { get; set; }

        /// <summary>
        /// A human-readable name for the road event
        /// </summary>
        [JsonProperty("name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("event_type", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public EventType EventType { get; set; }

        /// <summary>
        /// A list describing one or more road events which are related to this road event, such as a work zone project it is part of or another road event that occurs before or after it in sequence
        /// </summary>
        [JsonProperty("related_road_events", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<RelatedRoadEvent> RelatedRoadEvents { get; set; }

        [Obsolete("Depreciated, use the new `related_road_events` property instead")]
        [JsonProperty("relationship", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Relationship Relationship { get; set; }

        /// <summary>
        /// A list of publicly known names of the road on which the event occurs. This may include the road number designated by a jurisdiction such as a county, state or interstate (e.g. I-5, VT 133)
        /// </summary>
        [JsonProperty("road_names", Required = Required.Always)]
        [Required]
        [MinLength(1)]
        public ICollection<string> RoadNames { get; set; } = new Collection<string>();

        [JsonProperty("direction", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Direction Direction { get; set; }

        /// <summary>
        /// Short free text description of the road event
        /// </summary>
        [JsonProperty("description", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        /// <summary>
        /// The UTC date and time (formatted according to RFC 3339, Section 5.6) when the road event was created (e.g. 2020-11-03T19:37:00Z)
        /// </summary>
        [JsonProperty("creation_date", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? CreationDate { get; set; }

        /// <summary>
        /// The UTC date and time (formatted according to RFC 3339, Section 5.6) when the road event was last updated (e.g. 2020-11-03T19:37:00Z)
        /// </summary>
        [JsonProperty("update_date", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? UpdateDate { get; set; }

        private IDictionary<string, object> _additionalProperties = new Dictionary<string, object>();

        [JsonExtensionData]
        public IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties; }
            set { _additionalProperties = value; }
        }
    }
}