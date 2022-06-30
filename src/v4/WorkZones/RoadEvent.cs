using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Wsdot.Wzdx.v4.WorkZones
{
    public interface IRoadEvent
    {
        RoadEventCoreDetails CoreDetails { get; }
    }

    public abstract class RoadEvent : IRoadEvent
    {
        [JsonProperty("core_details", Required = Required.Always)]
        [Required]
        public RoadEventCoreDetails CoreDetails { get; set; } = new RoadEventCoreDetails();

        private IDictionary<string, object> _additionalProperties = new Dictionary<string, object>();

        [JsonExtensionData]
        public IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties; }
            set { _additionalProperties = value; }
        }
    }
}