using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.GeoJson.Geometries;

namespace Wsdot.Wzdx.v4.Devices
{
    /// <summary>
    /// Provides a builder for a v4 FieldDeviceFeature (FlashingBeacon) class
    /// </summary>
    public sealed class FlashingBeaconFeatureBuilder : 
        FieldDeviceFeatureBuilder<FlashingBeaconFeatureBuilder, FlashingBeacon>
    {
        public FlashingBeaconFeatureBuilder(string sourceId, string featureId, string roadName) :
            base(new DelegatingFactory<FieldDeviceFeature>(() => new FieldDeviceFeature() { Properties = new FlashingBeacon() }))
        {
            FeatureConfiguration.Set(feature => feature.Id, featureId);
            CoreDetailConfiguration.Set(details => details.DataSourceId, sourceId);
            WithGeometry(Point.FromCoordinates(Position.From(0, 0)));
            WithRoadName(roadName);
            WithStatus(FieldDeviceStatus.Unknown);
            WithFunction(FlashingBeaconFunction.WorkersPresent);
        }

        public FlashingBeaconFeatureBuilder WithFunction(FlashingBeaconFunction value)
        {
            PropertiesConfiguration.Set(properties => properties.Function, value);
            return Derived();
        }

        public FlashingBeaconFeatureBuilder WithFlashing(bool value)
        {
            PropertiesConfiguration.Set(properties => properties.IsFlashing, value);
            return Derived();
        }
    }
}