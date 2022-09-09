using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wzdx.v4.WorkZones
{
    /// <summary>
    /// Identifies a road event that is related to the road event that the RelatedRoadEvent object occurs on
    /// </summary>
    public class RelatedRoadEvent
    {
        /// <summary>
        /// The type of road event being identified, such as another sequence of related work zones, a detour, or next road event in sequence
        /// </summary>
        [JsonProperty("type", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public RelatedRoadEventType Type { get; set; }

        /// <summary>
        /// An identifier for the related road event by the type property
        /// </summary>
        [JsonProperty("id", Required = Required.Always)]
        [Required(AllowEmptyStrings = false)]
        public string Id { get; set; }

    }
}