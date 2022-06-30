using System;
using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.GeoJson.Geometries;

namespace Wsdot.Wzdx.v4.Devices
{
    /// <summary>
    /// Provides a builder for a v4 FieldDeviceFeature (TrafficSensor) class
    /// </summary>
    public class TrafficSensorFeatureBuilder :
        FieldDeviceFeatureBuilder<TrafficSensorFeatureBuilder, TrafficSensor>
    {
        public TrafficSensorFeatureBuilder(string sourceId, string featureId, string roadName) :
            base(new DelegatingFactory<FieldDeviceFeature>(() => new FieldDeviceFeature() { Properties = new TrafficSensor() }))
        {
            FeatureConfiguration.Set(feature => feature.Id, featureId);
            CoreDetailConfiguration.Set(details => details.DataSourceId, sourceId);
            WithGeometry(Point.FromCoordinates(Position.From(0, 0)));
            WithRoadName(roadName);
            WithStatus(FieldDeviceStatus.Unknown);
            WithCollectionInterval(DateTimeOffset.MinValue, DateTimeOffset.MinValue);
        }

        public TrafficSensorFeatureBuilder WithCollectionInterval(DateTimeOffset start, DateTimeOffset end)
        {
            PropertiesConfiguration.Set(properties => properties.CollectionIntervalStartDate, start);
            PropertiesConfiguration.Set(properties => properties.CollectionIntervalEndDate, end);
            return Derived();
        }

        public TrafficSensorFeatureBuilder WithAverageSpeedKph(int value)
        {
            PropertiesConfiguration.Set(properties => properties.AverageSpeedKph, value);
            return Derived();
        }

        public TrafficSensorFeatureBuilder WithOccupancyPercent(int value)
        {
            PropertiesConfiguration.Set(properties => properties.OccupancyPercent, value);
            return Derived();
        }

        public TrafficSensorFeatureBuilder WithLaneData(Func<IBuilder<TrafficSensorLaneData>, IBuilder<TrafficSensorLaneData>> config)
        {
            var value = config(new TrafficSensorLaneDataBuilder()).Result();
            PropertiesConfiguration.Combine(properties => properties.LaneData, properties => properties.LaneData.Add(value));
            return Derived();
        }
    }
}