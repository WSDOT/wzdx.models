using System;
using Wsdot.Wzdx.GeoJson.Geometries;
using Wsdot.Wzdx.v4.Feeds;

namespace Wsdot.Wzdx.v4.Devices
{

    //public interface IFieldDeviceFeatureBuilderFactory
    //{
    //    ArrowBoardFeatureBuilder ArrowBoard(string roadName, Point geometry);
    //    CameraFeatureBuilder Camera(string roadName, Point geometry);
    //    DynamicMessageSignFeatureBuilder DynamicMessageSign(string roadName, Point geometry);
    //    FlashingBeaconFeatureBuilder FlashingBeacon(string roadName, Point geometry, FlashingBeaconFunction function);
    //    HybridSignFeatureBuilder HybridSign(string roadName, Point geometry, HybridSignDynamicMessageFunction function);
    //    LocationMarkerFeatureBuilder LocationMarker(string roadName,
    //        Point geometry,
    //        Func<MarkedLocationBuilder, MarkedLocationBuilder> locationSetup);

    //    TrafficSensorFeatureBuilder TrafficSensor(string roadName, Point geometry);
    //}

    public static class DeviceFeatureBuilderFactoryExtensions
    {

        public static ArrowBoardFeatureBuilder ArrowBoard(this IFieldDeviceFeatureBuilderFactory factory, string roadName, Point geometry)
        {
            return new ArrowBoardFeatureBuilder(factory.SourceId, factory.FeatureId, roadName)
                .WithGeometry(geometry);
        }

        public static CameraFeatureBuilder Camera(this IFieldDeviceFeatureBuilderFactory factory, string roadName, Point geometry)
        {
            return new CameraFeatureBuilder(factory.SourceId, factory.FeatureId, roadName)
                .WithGeometry(geometry);
        }

        public static DynamicMessageSignFeatureBuilder DynamicMessageSign(this IFieldDeviceFeatureBuilderFactory factory, string roadName, Point geometry)
        {
            return new DynamicMessageSignFeatureBuilder(factory.SourceId, factory.FeatureId, roadName)
                .WithGeometry(geometry);
        }

        public static FlashingBeaconFeatureBuilder FlashingBeacon(this IFieldDeviceFeatureBuilderFactory factory, string roadName, Point geometry, FlashingBeaconFunction function)
        {
            return new FlashingBeaconFeatureBuilder(factory.SourceId, factory.FeatureId, roadName)
                .WithFunction(function)
                .WithGeometry(geometry);
        }

        public static HybridSignFeatureBuilder HybridSign(this IFieldDeviceFeatureBuilderFactory factory, string roadName, Point geometry,
            HybridSignDynamicMessageFunction function)
        {
            return new HybridSignFeatureBuilder(factory.SourceId, factory.FeatureId, roadName)
                .WithFunction(function)
                .WithGeometry(geometry);
        }

        public static LocationMarkerFeatureBuilder LocationMarker(this IFieldDeviceFeatureBuilderFactory factory, string roadName, Point geometry,
            Func<MarkedLocationBuilder, MarkedLocationBuilder> locationSetup)
        {
            return new LocationMarkerFeatureBuilder(factory.SourceId, factory.FeatureId, roadName)
                .WithMarkedLocation(locationSetup)
                .WithGeometry(geometry);
        }

        public static TrafficSensorFeatureBuilder TrafficSensor(this IFieldDeviceFeatureBuilderFactory factory, string roadName, Point geometry)
        {
            return new TrafficSensorFeatureBuilder(factory.SourceId, factory.FeatureId, roadName)
                .WithGeometry(geometry);
        }
    }
}
