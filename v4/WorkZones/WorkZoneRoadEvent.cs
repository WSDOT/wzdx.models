using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wsdot.Wzdx.v4.WorkZones
{
    /// <summary>
    /// Describes a work zone road event including where, when, and what activities are taking place within a work zone on a roadway
    /// </summary>
    public class WorkZoneRoadEvent : RoadEvent
    {
        /// <summary>
        /// Name or number of the nearest cross street along the roadway where the event begins
        /// </summary>
        [JsonProperty("beginning_cross_street", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string BeginningCrossStreet { get; set; }

        /// <summary>
        /// Name or number of the nearest cross street along the roadway where the event ends
        /// </summary>
        [JsonProperty("ending_cross_street", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string EndingCrossStreet { get; set; }

        /// <summary>
        /// The linear distance measured against a milepost marker along a roadway where the event begins
        /// </summary>
        [JsonProperty("beginning_milepost", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [Range(0D, double.MaxValue)]
        public double BeginningMilepost { get; set; }

        /// <summary>
        /// The linear distance measured against a milepost marker along a roadway where the event ends
        /// </summary>
        [JsonProperty("ending_milepost", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [Range(0D, double.MaxValue)]
        public double EndingMilepost { get; set; }

        [JsonProperty("beginning_accuracy", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public SpatialVerification BeginningAccuracy { get; set; }

        [JsonProperty("ending_accuracy", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public SpatialVerification EndingAccuracy { get; set; }

        /// <summary>
        /// The UTC date and time (formatted according to RFC 3339, Section 5.6) when the road event begins (e.g. 2020-11-03T19:37:00Z)
        /// </summary>
        [JsonProperty("start_date", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public System.DateTimeOffset StartDate { get; set; }

        /// <summary>
        /// The UTC date and time (formatted according to RFC 3339, Section 5.6) when the road event ends (e.g. 2020-11-03T19:37:00Z)
        /// </summary>
        [JsonProperty("end_date", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public System.DateTimeOffset EndDate { get; set; }

        [JsonProperty("start_date_accuracy", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public TimeVerification StartDateAccuracy { get; set; }

        [JsonProperty("end_date_accuracy", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public TimeVerification EndDateAccuracy { get; set; }

        [JsonProperty("event_status", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public EventStatus EventStatus { get; set; }

        [JsonProperty("vehicle_impact", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public VehicleImpact VehicleImpact { get; set; }

        [JsonProperty("location_method", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public LocationMethod LocationMethod { get; set; }

        [JsonProperty("worker_presence", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public WorkerPresence WorkerPresence { get; set; }

        /// <summary>
        /// If applicable, the reduced speed limit posted within the road event, in kilometers per hour
        /// </summary>
        [JsonProperty("reduced_speed_limit_kph", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [Range(0D, double.MaxValue)]
        public double? ReducedSpeedLimitKph { get; set; }

        /// <summary>
        /// A list of zero or more restrictions applying to the road event
        /// </summary>
        [JsonProperty("restrictions", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<Restriction> Restrictions { get; set; }

        /// <summary>
        /// A list of the types of work being done in a road event
        /// </summary>
        [JsonProperty("types_of_work", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<TypeOfWork> TypesOfWork { get; set; }

        /// <summary>
        /// A list of individual lanes within a road event (roadway segment)
        /// </summary>
        [JsonProperty("lanes", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<Lane> Lanes { get; set; }
    }
}