using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wzdx.v4.WorkZones
{
    /// <summary>
    /// A road event describing a section of roadway and the limitations of how that section can be used
    /// </summary>
    public class IncidentRoadEvent : RoadEvent
    {
        public IncidentRoadEvent()
        {
            CoreDetails.EventType = EventType.Incident;
        }

        // see: https://github.com/usdot-jpo-ode/TDx/blob/main/spec-content/objects/IncidentRoadEvent.md

        // A list of one or more incident types describing the cause of closure or restriction.
        // Incident types are described using incident enumerations.
        [JsonProperty("types_of_incident", Required = Required.Always)]
        public ICollection<TypeOfIncident> TypesOfIncident { get; set; } = new HashSet<TypeOfIncident>();

        /// <summary>
        /// The UTC date and time (formatted according to RFC 3339, Section 5.6) when the road event begins (e.g. 2020-11-03T19:37:00Z)
        /// </summary>
        [JsonProperty("start_date", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public System.DateTimeOffset StartDate { get; set; }

        /// <summary>
        /// The UTC date and time (formatted according to RFC 3339, Section 5.6) when the road event ends (e.g. 2020-11-03T19:37:00Z)
        /// </summary>
        [JsonProperty("end_date", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        //[Required(AllowEmptyStrings = true, ErrorMessage = "blah blah blah")]
        public System.DateTimeOffset? EndDate { get; set; }

        //is_start_date_verified Boolean Indicates if the incident has been confirmed to have started, such as from a person or device in the field or a report from a traffic management center.
        [JsonProperty("is_start_date_verified", Required = Required.Always)]
        public bool IsStartDateVerified { get; set; }

        //Indicates if the incident has been confirmed to have ended, such as from a person or device in the field or a report from a traffic management center,
        [JsonProperty("is_end_date_verified", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsEndDateVerified { get; set; }

        //Indicates if the start position (first geometric coordinate pair) is based on actual reported data from a GPS-equipped device that measured the location of the start of the incident.
        [JsonProperty("is_start_position_verified", Required = Required.Always)]
        public bool IsStartPositionVerified { get; set; }

        // Indicates if the end position (last geometric coordinate pair) is based on actual reported data from a GPS-equipped device that measured the location of the end of the incident.
        [JsonProperty("is_end_position_verified", Required = Required.Always)]
        public bool IsEndPositionVerified { get; set; }
        
        // The impact to vehicular lanes along a single road in a single direction.
        [JsonProperty("vehicle_impact", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public VehicleImpact VehicleImpact { get; set; }

        /// <summary>
        /// A list of individual lanes within a road event (roadway segment)
        /// </summary>
        [JsonProperty("lanes", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<Lane> Lanes { get; set; } = new HashSet<Lane>();

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
        /// The linear distance measured against a milepost marker along a roadway where the event begins.
        /// A milepost or mile marker is a surveyed distance posted along a roadway measuring the length (in miles or tenth of a mile) from the south-west to the north-east.
        /// These markers are typically notated on State and local government digital road networks
        /// </summary>
        [JsonProperty("beginning_milepost", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [Range(0D, double.MaxValue)]
        public double? BeginningMilepost { get; set; }

        /// <summary>
        /// The linear distance measured against a milepost marker along a roadway where the event ends.
        /// A milepost or mile marker is a surveyed distance posted along a roadway measuring the length (in miles or tenth of a mile) from the south-west to the north-east.
        /// These markers are typically notated on State and local government digital road networks.
        /// </summary>
        [JsonProperty("ending_milepost", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [Range(0D, double.MaxValue)]
        public double? EndingMilepost { get; set; }

        /// <summary>
        /// If applicable, the reduced speed limit posted within the road event, in kilometers per hour
        /// </summary>
        [JsonProperty("reduced_speed_limit_kph", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [Range(0, int.MaxValue)]
        public int? ReducedSpeedLimitKph { get; set; }

        /// <summary>
        /// A list of zero or more restrictions that apply to the roadway segment described by this road event.
        /// Optional Restrictions can also be provided on an individual lane.
        /// </summary>
        [JsonProperty("restrictions", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<Restriction> Restrictions { get; set; } = new HashSet<Restriction>();
    }
}