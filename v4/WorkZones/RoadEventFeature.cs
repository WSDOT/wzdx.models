using Newtonsoft.Json;
using Wsdot.Wzdx.GeoJson;
using Wsdot.Wzdx.v4.WorkZones.Converters;

// ReSharper disable ClassNeverInstantiated.Global

namespace Wsdot.Wzdx.v4.WorkZones
{
    /// <summary>
    /// The container object for a specific WZDx road event; an instance of a GeoJSON Feature
    /// </summary>
    public class RoadEventFeature : Feature<IRoadEvent>
    {
        [JsonProperty("properties", Required = Required.Always)]
        [JsonConverter(typeof(RoadEventConverter))]
        public override IRoadEvent Properties { get; set; }
    }
}