using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.GeoJson.Geometries;

namespace Wsdot.Wzdx.v4.Devices
{
    public sealed class TrafficSensorFeatureBuilder : 
        FieldDeviceFeatureBuilder<TrafficSensorFeatureBuilder, TrafficSensor>
    {
        public TrafficSensorFeatureBuilder(string sourceId, string featureId, string roadName) :
            this(new List<Action<FieldDeviceFeature>>(), (feature, beacon) =>
            {
                var geometry = Point.FromCoordinates(Position.From(0, 0));
                feature.Id = featureId;
                feature.Geometry = geometry;
                feature.BoundaryBox = geometry.BoundaryBox.ToList().AsReadOnly();

                beacon.CoreDetails.DataSourceId = sourceId;
                beacon.CoreDetails.RoadNames.Add(roadName);
                beacon.CoreDetails.DeviceStatus = FieldDeviceStatus.Unknown;
            })
        {
            // ignore
        }

        private TrafficSensorFeatureBuilder(IEnumerable<Action<FieldDeviceFeature>> configuration, Action<FieldDeviceFeature, TrafficSensor> step) :
            base(configuration, step)
        {
            // ignore
        }

        protected override TrafficSensorFeatureBuilder CreateWith(Action<FieldDeviceFeature, TrafficSensor> step)
        {
            return new TrafficSensorFeatureBuilder(Configuration, step);
        }

        protected override Func<TrafficSensor> ResultProperties { get; } = () => new TrafficSensor();

    }
}