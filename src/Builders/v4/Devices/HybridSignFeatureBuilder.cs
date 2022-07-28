using Wzdx.Core;
using Wzdx.GeoJson.Geometries;

namespace Wzdx.v4.Devices
{
    /// <summary>
    /// Provides a builder for a v4 FieldDeviceFeature (HybridSign) class
    /// </summary>
    public class HybridSignFeatureBuilder :
        FieldDeviceFeatureBuilder<HybridSignFeatureBuilder, HybridSign>
    {
        public HybridSignFeatureBuilder(string sourceId, string featureId, string roadName) :
            base(new DelegatingFactory<FieldDeviceFeature>(() => new FieldDeviceFeature() { Properties = new HybridSign() }))
        {
            FeatureConfiguration.Set(feature => feature.Id, featureId);
            CoreDetailConfiguration.Set(details => details.DataSourceId, sourceId);
            WithGeometry(Point.FromCoordinates(Position.From(0, 0)));
            WithRoadName(roadName);
            WithStatus(FieldDeviceStatus.Unknown);
            WithFunction(HybridSignDynamicMessageFunction.Other);
        }
        
        public HybridSignFeatureBuilder WithFunction(HybridSignDynamicMessageFunction value)
        {
            PropertiesConfiguration.Set(properties => properties.DynamicMessageFunction, value);
            return Derived();
        }

        public HybridSignFeatureBuilder WithDynamicMessage(string value)
        {
            PropertiesConfiguration.Set(properties => properties.DynamicMessageText, value);
            return Derived();
        }

        public HybridSignFeatureBuilder WithStaticMessage(string value)
        {
            PropertiesConfiguration.Set(properties => properties.StaticSignText, value);
            return Derived();
        }
    }
}