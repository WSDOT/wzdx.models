using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.GeoJson.Geometries;

namespace Wsdot.Wzdx.v4.Devices
{
    /// <summary>
    /// Provides a builder of a v4 FieldDeviceFeature (DynamicMessageSign) class
    /// </summary>
    public sealed class DynamicMessageSignFeatureBuilder : FieldDeviceFeatureBuilder<DynamicMessageSignFeatureBuilder, DynamicMessageSign>
    {

        public DynamicMessageSignFeatureBuilder(string sourceId, string featureId, string roadName) :
            base(new DelegatingFactory<FieldDeviceFeature>(() => new FieldDeviceFeature() { Properties = new DynamicMessageSign() }))
        {
            FeatureConfiguration.Set(feature => feature.Id, featureId);
            CoreDetailConfiguration.Set(details => details.DataSourceId, sourceId);
            WithGeometry(Point.FromCoordinates(Position.From(0, 0)));
            WithRoadName(roadName);
            WithStatus(FieldDeviceStatus.Unknown);
            WithMessage(string.Empty);
        }

        public DynamicMessageSignFeatureBuilder WithMessage(string value)
        {
            PropertiesConfiguration.Set(properties => properties.MessageMultiString, value);
            return Derived();
        }
    }
}