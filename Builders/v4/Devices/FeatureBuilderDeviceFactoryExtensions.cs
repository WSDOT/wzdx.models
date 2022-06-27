using System;
using System.Diagnostics.Contracts;
using Wsdot.Wzdx.GeoJson.Geometries;
using Wsdot.Wzdx.v4.Feeds;

namespace Wsdot.Wzdx.v4.Devices
{
    /// <summary>
    /// Provides extensions of for v4 IFieldDeviceFeatureBuilderFactory 
    /// </summary>
    public static class DeviceFeatureBuilderFactoryExtensions
    {
        [Pure]
        public static ArrowBoardFeatureBuilder ArrowBoard(this IFieldDeviceFeatureBuilderFactory factory, string roadName, Point geometry)
        {
            return new ArrowBoardFeatureBuilder(factory.SourceId, factory.FeatureId, roadName)
                .WithGeometry(geometry);
        }

        [Pure]
        public static CameraFeatureBuilder Camera(this IFieldDeviceFeatureBuilderFactory factory, string roadName, Point geometry)
        {
            return new CameraFeatureBuilder(factory.SourceId, factory.FeatureId, roadName)
                .WithGeometry(geometry);
        }

        [Pure]
        public static DynamicMessageSignFeatureBuilder DynamicMessageSign(this IFieldDeviceFeatureBuilderFactory factory, string roadName, Point geometry)
        {
            return new DynamicMessageSignFeatureBuilder(factory.SourceId, factory.FeatureId, roadName)
                .WithGeometry(geometry);
        }

        [Pure]
        public static FlashingBeaconFeatureBuilder FlashingBeacon(this IFieldDeviceFeatureBuilderFactory factory, string roadName, Point geometry, FlashingBeaconFunction function)
        {
            return new FlashingBeaconFeatureBuilder(factory.SourceId, factory.FeatureId, roadName)
                .WithFunction(function)
                .WithGeometry(geometry);
        }

        [Pure]
        public static HybridSignFeatureBuilder HybridSign(this IFieldDeviceFeatureBuilderFactory factory, string roadName, Point geometry,
            HybridSignDynamicMessageFunction function)
        {
            return new HybridSignFeatureBuilder(factory.SourceId, factory.FeatureId, roadName)
                .WithFunction(function)
                .WithGeometry(geometry);
        }

        [Pure]
        public static LocationMarkerFeatureBuilder LocationMarker(this IFieldDeviceFeatureBuilderFactory factory, string roadName, Point geometry,
            Func<MarkedLocationBuilder, MarkedLocationBuilder> locationSetup)
        {
            return new LocationMarkerFeatureBuilder(factory.SourceId, factory.FeatureId, roadName)
                .WithMarkedLocation(locationSetup)
                .WithGeometry(geometry);
        }

        [Pure]
        public static TrafficSensorFeatureBuilder TrafficSensor(this IFieldDeviceFeatureBuilderFactory factory, string roadName, Point geometry)
        {
            return new TrafficSensorFeatureBuilder(factory.SourceId, factory.FeatureId, roadName)
                .WithGeometry(geometry);
        }
    }
}
