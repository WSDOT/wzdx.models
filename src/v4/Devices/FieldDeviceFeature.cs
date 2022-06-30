using Newtonsoft.Json;
using Wsdot.Wzdx.GeoJson;
using Wsdot.Wzdx.v4.Devices.Converters;

// ReSharper disable ClassNeverInstantiated.Global

namespace Wsdot.Wzdx.v4.Devices
{
    /// <summary>
    /// The GeoJSON feature container for a WZDx field device
    /// </summary>
    public class FieldDeviceFeature : Feature<IFieldDevice>
    {
        [JsonProperty("properties", Required = Required.Always)]
        [JsonConverter(typeof(FieldDeviceFeatureConverter))]
        public override IFieldDevice Properties { get; set; }
    }

    public interface IFieldDevice
    {
        FieldDeviceCoreDetails CoreDetails { get; }
    }
}