using System;
using Wzdx.Core;
using Wzdx.GeoJson.Geometries;

namespace Wzdx.v4.Devices
{
    /// <summary>
    /// Provides a builder for a v4 FieldDeviceFeature (TrafficSignal) class
    /// </summary>
    public sealed class TrafficSignalFeatureBuilder : FieldDeviceFeatureBuilder<TrafficSignalFeatureBuilder, TrafficSignal>
    {
        public TrafficSignalFeatureBuilder(string sourceId, string featureId, string roadName) :
            base(new DelegatingFactory<FieldDeviceFeature>(() => new FieldDeviceFeature() { Properties = new TrafficSignal() }))
        {
            FeatureConfiguration.Set(feature => feature.Id, featureId);
            CoreDetailConfiguration.Set(details => details.DataSourceId, sourceId);
            WithGeometry(Point.FromCoordinates(Position.From(0, 0)));
            WithRoadName(roadName);
            WithStatus(FieldDeviceStatus.Unknown);
        }

        public TrafficSignalFeatureBuilder WithMode(TrafficSignalMode value)
        {
            PropertiesConfiguration.Set(properties => properties.Mode, value);
            return Derived();
        }
    }
}