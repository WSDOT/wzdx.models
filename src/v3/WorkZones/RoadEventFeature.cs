using System.Collections.Generic;
using Newtonsoft.Json;
using Wzdx.GeoJson;

namespace Wzdx.v3.WorkZones
{
    /// <summary>The container object for a WZDx road event; an instance of a GeoJSON Feature</summary>

    public class RoadEventFeature  : Feature<RoadEvent>
    {
        public override RoadEvent Properties { get; set; } = new RoadEvent();
        
        private IDictionary<string, object> _additionalProperties = new Dictionary<string, object>();

        [JsonExtensionData]
        public IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties; }
            set { _additionalProperties = value; }
        }
    }
}