using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Wsdot.Wzdx.GeoJson;
using Wsdot.Wzdx.v3.WorkZones;

namespace Wsdot.Wzdx.v3.Feeds
{
    public class RoadEventsFeed : FeatureCollection<RoadEventFeature>
    {
        [JsonProperty("road_event_feed_info", Required = Required.Always)]
        [Required]
        public RoadEventFeedInfo FeedInfo { get; set; } = new RoadEventFeedInfo();
        
        private IDictionary<string, object> _additionalProperties = new Dictionary<string, object>();

        [JsonExtensionData]
        public IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties; }
            set { _additionalProperties = value; }
        }
    }
}