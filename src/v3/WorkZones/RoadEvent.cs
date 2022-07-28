using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wzdx.v3.WorkZones
{
    /// <summary>Describes an activity taking place along a road segment</summary>

    public class RoadEvent 
    {
        /// <summary>Identifies the data source from which the road event data is sourced from</summary>
        [JsonProperty("data_source_id", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string DataSourceId { get; set; }

        [JsonProperty("event_type", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public EventType? EventType { get; set; }

        [JsonProperty("relationship", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Relationship Relationship { get; set; }

        /// <summary>A list of publicly known names of the road on which the event occurs. This may include the road number designated by a jurisdiction such as a county, state or interstate (e.g. I-5, VT 133)</summary>
        [JsonProperty("road_names", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [MinLength(1)]
        public ICollection<string> RoadNames { get; set; }

        [JsonProperty("direction", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Direction Direction { get; set; }

        /// <summary>Name or number of the nearest cross street along the roadway where the event begins</summary>
        [JsonProperty("beginning_cross_street", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string BeginningCrossStreet { get; set; }

        /// <summary>Name or number of the nearest cross street along the roadway where the event ends</summary>
        [JsonProperty("ending_cross_street", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string EndingCrossStreet { get; set; }

        /// <summary>The linear distance measured against a milepost marker along a roadway where the event begins</summary>
        [JsonProperty("beginning_milepost", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [Range(0D, double.MaxValue)]
        public double? BeginningMilepost { get; set; }

        /// <summary>The linear distance measured against a milepost marker along a roadway where the event ends</summary>
        [JsonProperty("ending_milepost", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [Range(0D, double.MaxValue)]
        public double? EndingMilepost { get; set; }

        [JsonProperty("beginning_accuracy", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public SpatialVerification BeginningAccuracy { get; set; }

        [JsonProperty("ending_accuracy", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public SpatialVerification EndingAccuracy { get; set; }

        /// <summary>The UTC date and time (formatted according to RFC 3339, Section 5.6) when the road event begins (e.g. 2020-11-03T19:37:00Z)</summary>
        [JsonProperty("start_date", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public DateTimeOffset StartDate { get; set; }

        /// <summary>The UTC date and time (formatted according to RFC 3339, Section 5.6) when the road event ends (e.g. 2020-11-03T19:37:00Z)</summary>
        [JsonProperty("end_date", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public DateTimeOffset EndDate { get; set; }

        [JsonProperty("start_date_accuracy", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public TimeVerification StartDateAccuracy { get; set; }

        [JsonProperty("end_date_accuracy", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public TimeVerification EndDateAccuracy { get; set; }

        [JsonProperty("event_status", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public EventStatus? EventStatus { get; set; }

        [JsonProperty("vehicle_impact", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public VehicleImpact VehicleImpact { get; set; }

        /// <summary>A flag indicating that there are workers present in the road event</summary>
        [JsonProperty("workers_present", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? WorkersPresent { get; set; }

        /// <summary>The reduced speed limit posted within the road event</summary>
        [JsonProperty("reduced_speed_limit", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [Range(0, int.MaxValue)]
        public int? ReducedSpeedLimit { get; set; }

        /// <summary>Zero or more road restrictions applying to the road event</summary>
        [JsonProperty("restrictions", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore, ItemConverterType = typeof(StringEnumConverter))]
        public ICollection<RoadRestriction> Restrictions { get; set; }

        /// <summary>Short free text description of the road event</summary>
        [JsonProperty("description", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        /// <summary>The UTC date and time (formatted according to RFC 3339, Section 5.6) when the road event was created (e.g. 2020-11-03T19:37:00Z)</summary>
        [JsonProperty("creation_date", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? CreationDate { get; set; }

        /// <summary>The UTC date and time (formatted according to RFC 3339, Section 5.6) when the road event was last updated (e.g. 2020-11-03T19:37:00Z)</summary>
        [JsonProperty("update_date", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? UpdateDate { get; set; }

        /// <summary>A list of the types of work being done in a road event</summary>
        [JsonProperty("types_of_work", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<TypeOfWork> TypesOfWork { get; set; }

        /// <summary>A list of individual lanes within a road event (roadway segment)</summary>
        [JsonProperty("lanes", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<Lane> Lanes { get; set; }

        /// <summary>***DEPRECATED*** A unique identifier issued by the data feed provider to identify the work zone project or activity</summary>
        [JsonProperty("road_event_id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string RoadEventId { get; set; }

        /// <summary>***DEPRECATED*** The road number designated by a jurisdiction such as a county, state or interstate (e.g. I-5, VT 133)</summary>
        [JsonProperty("road_number", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string RoadNumber { get; set; }

        /// <summary>***DEPRECATED*** Publicly known name of the road on which the event occurs</summary>
        [JsonProperty("road_name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string RoadName { get; set; }

        /// <summary>***DEPRECATED*** The total number of lanes associated with the road event</summary>
        [JsonProperty("total_num_lanes", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? TotalNumLanes { get; set; }

        private IDictionary<string, object> _additionalProperties = new Dictionary<string, object>();

        [JsonExtensionData]
        public IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties; }
            set { _additionalProperties = value; }
        }


    }
}