using Wzdx.Core;
using Wzdx.GeoJson.Geometries;

namespace Wzdx.v4.Devices
{
    /// <summary>
    /// Provides a builder for a v4 FieldDeviceFeature (ArrowBoard) class
    /// </summary>
    public sealed class ArrowBoardFeatureBuilder : FieldDeviceFeatureBuilder<ArrowBoardFeatureBuilder, ArrowBoard>
    {
        public ArrowBoardFeatureBuilder(string sourceId, string featureId, string roadName) :
            base(new DelegatingFactory<FieldDeviceFeature>(() => new FieldDeviceFeature() { Properties = new ArrowBoard() }))
        {
            FeatureConfiguration.Set(feature => feature.Id, featureId);
            CoreDetailConfiguration.Set(details => details.DataSourceId, sourceId);
            WithGeometry(Point.FromCoordinates(Position.From(0, 0)));
            WithRoadName(roadName);
            WithStatus(FieldDeviceStatus.Unknown);
        }

        public ArrowBoardFeatureBuilder WithIsMoving(bool value)
        {
            PropertiesConfiguration.Set(properties => properties.IsMoving, value);
            return Derived();
        }

        public ArrowBoardFeatureBuilder WithIsInTransportPosition(bool value)
        {
            PropertiesConfiguration.Set(properties => properties.IsInTransportPosition, value);
            return Derived();
        }

        public ArrowBoardFeatureBuilder WithPattern(ArrowBoardPattern value)
        {
            PropertiesConfiguration.Set(properties => properties.Pattern, value);
            return Derived();
        }
    }
}