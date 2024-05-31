using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wzdx.v4.WorkZones
{
    /// <summary>
    /// Information about the presence of workers in the work zone event area
    /// </summary>
    public class WorkerPresence
    {
        /// <summary>
        /// Whether workers are present in the work zone event area, following the definition provided in the ‘definition’ property on the WorkerPresence object
        /// </summary>
        [JsonProperty("are_workers_present", Required = Required.Always)]
        public bool AreWorkersPresent { get; set; }

        [JsonProperty("method", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public WorkerPresenceMethod? Method { get; set; }

        /// <summary>
        /// The UTC date and time at which the presence of workers was last confirmed
        /// </summary>
        [JsonProperty("worker_presence_last_confirmed_date", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? WorkerPresenceLastConfirmedDate { get; set; }

        [JsonProperty("confidence", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public WorkerPresenceConfidence? Confidence { get; set; }

        /// <summary>
        /// A list of situations in which workers are considered to be present in the jurisdiction of the data provider
        /// </summary>
        [JsonProperty("definition", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore, ItemConverterType = typeof(StringEnumConverter))]
        public ICollection<WorkerPresenceDefinition> Definition { get; set; } = new HashSet<WorkerPresenceDefinition>();
    }
}