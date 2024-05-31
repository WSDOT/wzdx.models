using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wzdx.v4.WorkZones
{
    /// <summary>
    /// Describes an event that causes disruptions to expected operations.
    /// </summary>
    public class TypeOfIncident
    {
        /// <summary>
        /// The category of incident causing disruptions.
        /// </summary>
        [JsonProperty("incident_category", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public IncidentCategory IncidentCategory { get; set; }

        /// <summary>
        /// The type incident causing disruptions.
        /// Value must belong to the IncidentCategory indicated with incident_category
        /// see Category column of IncidentType table.
        /// </summary>
        [JsonProperty("incident_type", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public IncidentType IncidentType { get; set; }

        /// <summary>
        /// Short free text description of the type of incident
        /// </summary>
        [JsonProperty("description", Required = Required.Always)]
        public string Description { get; set; }
    }
}