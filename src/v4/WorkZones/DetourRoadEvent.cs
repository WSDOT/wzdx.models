using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wzdx.v4.WorkZones
{
    /// <summary>
    /// Describes a detour on a roadway
    /// </summary>
    public class DetourRoadEvent : RoadEvent
    {
        public DetourRoadEvent()
        {
            CoreDetails.EventType = EventType.Detour;
        }

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
        public double? BeginningMilepost { get; set; }

        /// <summary>
        /// The linear distance measured against a milepost marker along a roadway where the event ends
        /// </summary>
        [JsonProperty("ending_milepost", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [Range(0D, double.MaxValue)]
        public double? EndingMilepost { get; set; }

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
        public EventStatus? EventStatus { get; set; }


    }
}