using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.GeoJson.Geometries;
using Wsdot.Wzdx.v4.Devices;

namespace Wsdot.Wzdx.v4.Builders
{
    public sealed class LocationMarkerFeatureBuilder : 
        FieldDeviceFeatureBuilder<LocationMarkerFeatureBuilder, LocationMarker>
    {

        public LocationMarkerFeatureBuilder(string sourceId, string featureId, string roadName) : base(new List<Action<FieldDeviceFeature>>(),
            (feature, properties) =>
            {
                var geometry = Point.FromCoordinates(Position.From(0, 0));
                feature.Id = featureId;
                feature.Geometry = geometry;
                feature.BoundaryBox = geometry.BoundaryBox.ToList().AsReadOnly();

                properties.CoreDetails.DataSourceId = sourceId;
                properties.CoreDetails.RoadNames.Add(roadName);
                properties.CoreDetails.DeviceStatus = FieldDeviceStatus.Unknown;
                properties.MarkedLocations.Add(new MarkedLocation() { RoadEventId = null, Type = MarkedLocationType.TemporaryTrafficSignal });
            })
        {
            // ignore
        }

        private LocationMarkerFeatureBuilder(IEnumerable<Action<FieldDeviceFeature>> configuration, Action<FieldDeviceFeature, LocationMarker> step) : base(configuration, step)
        {
            // ignore
        }

        public LocationMarkerFeatureBuilder WithMarkedLocation(Func<MarkedLocationBuilder, MarkedLocationBuilder> config)
        {
            var value = config(new MarkedLocationBuilder()).Result();
            return CreateWith((feature, marker) => marker.MarkedLocations = new List<MarkedLocation>() { value });
        }
        public LocationMarkerFeatureBuilder WithAdditionalMarkedLocation(Func<MarkedLocationBuilder, MarkedLocationBuilder> config)
        {
            var value = config(new MarkedLocationBuilder()).Result();
            return CreateWith((feature, marker) => marker.MarkedLocations.Add(value));
        }

        protected override LocationMarkerFeatureBuilder CreateWith(Action<FieldDeviceFeature, LocationMarker> step)
        {
            return new LocationMarkerFeatureBuilder(Configuration, step);
        }

        protected override Func<LocationMarker> ResultProperties { get; } =
            () => new LocationMarker();
    }
}