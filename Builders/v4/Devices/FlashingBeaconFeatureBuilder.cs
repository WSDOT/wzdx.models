using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Wsdot.Wzdx.GeoJson.Geometries;

namespace Wsdot.Wzdx.v4.Devices
{
    /// <summary>
    /// Provides an immutable builder of a v4 FieldDeviceFeature (FlashingBeacon) class
    /// </summary>
    public sealed class FlashingBeaconFeatureBuilder : 
        FieldDeviceFeatureBuilder<FlashingBeaconFeatureBuilder, FlashingBeacon>
    {
        public FlashingBeaconFeatureBuilder(string sourceId, string featureId, string roadName) :
            this(new List<Action<FieldDeviceFeature>>(), (feature, beacon) =>
            {
                var geometry = Point.FromCoordinates(Position.From(0, 0));
                feature.Id = featureId;
                feature.Geometry = geometry;
                feature.BoundaryBox = geometry.BoundaryBox.ToList().AsReadOnly();

                beacon.CoreDetails.DataSourceId = sourceId;
                beacon.CoreDetails.RoadNames.Add(roadName);
                beacon.CoreDetails.DeviceStatus = FieldDeviceStatus.Unknown;
                beacon.Function = FlashingBeaconFunction.WorkersPresent;
            })
        {
            // ignore
        }

        [Pure]
        public FlashingBeaconFeatureBuilder WithFunction(FlashingBeaconFunction value)
        {
            return CreateWith((feature, beacon) => beacon.Function = value);
        }

        [Pure]
        public FlashingBeaconFeatureBuilder WithFlashing(bool value)
        {
            return CreateWith((feature, beacon) => beacon.IsFlashing = value);
        }

        private FlashingBeaconFeatureBuilder(IEnumerable<Action<FieldDeviceFeature>> configuration, Action<FieldDeviceFeature, FlashingBeacon> step) :
            base(configuration, step)
        {
            // ignore
        }

        protected override FlashingBeaconFeatureBuilder CreateWith(Action<FieldDeviceFeature, FlashingBeacon> step)
        {
            return new FlashingBeaconFeatureBuilder(Configuration, step);
        }

        protected override Func<FlashingBeacon> ResultProperties { get; } = () => new FlashingBeacon();

    }
}