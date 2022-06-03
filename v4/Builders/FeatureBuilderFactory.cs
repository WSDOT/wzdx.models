using System;
using Wsdot.Wzdx.GeoJson.Geometries;
using Wsdot.Wzdx.v4.Devices;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.Builders
{
    public interface IRoadRestrictionFeatureBuilderFactory
    {
        RoadRestrictionFeatureBuilder RoadRestriction(string roadName, Direction direction, LineString geometry);
        RoadRestrictionFeatureBuilder RoadRestriction(string roadName, Direction direction, MultiPoint geometry);
    }

    public interface IRoadEventFeatureBuilderFactory
    {
        WorkZoneRoadEventFeatureBuilder WorkZone(string roadName, Direction direction, LineString geometry);
        WorkZoneRoadEventFeatureBuilder WorkZone(string roadName, Direction direction, MultiPoint geometry);
        DetourRoadEventFeatureBuilder Detour(string roadName, Direction direction, LineString geometry);
        DetourRoadEventFeatureBuilder Detour(string roadName, Direction direction, MultiPoint geometry);
    }

    public interface IFieldDeviceFeatureBuilderFactory
    {
        ArrowBoardFeatureBuilder ArrowBoard(string roadName, Point geometry);
        CameraFeatureBuilder Camera(string roadName, Point geometry);
        DynamicMessageSignFeatureBuilder DynamicMessageSign(string roadName, Point geometry);
        FlashingBeaconFeatureBuilder FlashingBeacon(string roadName, Point geometry, FlashingBeaconFunction function);
        HybridSignFeatureBuilder HybridSign(string roadName, Point geometry, HybridSignDynamicMessageFunction function);
        LocationMarkerFeatureBuilder LocationMarker(string roadName,
            Point geometry,
            Func<MarkedLocationBuilder, MarkedLocationBuilder> locationSetup);

        TrafficSensorFeatureBuilder TrafficSensor(string roadName, Point geometry);
    }

    public class FeatureBuilderFactory : 
        IRoadEventFeatureBuilderFactory, 
        IRoadRestrictionFeatureBuilderFactory,
        IFieldDeviceFeatureBuilderFactory
    {
        public string FeatureId { get; }
        public string SourceId { get; }

        public FeatureBuilderFactory(string sourceId, string featureId)
        {
            SourceId = sourceId;
            FeatureId = featureId;
        }

        WorkZoneRoadEventFeatureBuilder IRoadEventFeatureBuilderFactory.WorkZone(string roadName, Direction direction, LineString geometry)
        {
            return new WorkZoneRoadEventFeatureBuilder(SourceId, FeatureId, roadName, direction)
                .WithGeometry(geometry);
        }

        WorkZoneRoadEventFeatureBuilder IRoadEventFeatureBuilderFactory.WorkZone(string roadName, Direction direction, MultiPoint geometry)
        {
            return new WorkZoneRoadEventFeatureBuilder(SourceId, FeatureId, roadName, direction)
                .WithGeometry(geometry);
        }

        DetourRoadEventFeatureBuilder IRoadEventFeatureBuilderFactory.Detour(string roadName, Direction direction, LineString geometry)
        {
            return new DetourRoadEventFeatureBuilder(SourceId, FeatureId, roadName, direction)
                .WithGeometry(geometry);
        }

        DetourRoadEventFeatureBuilder IRoadEventFeatureBuilderFactory.Detour(string roadName, Direction direction, MultiPoint geometry)
        {
            return new DetourRoadEventFeatureBuilder(SourceId, FeatureId, roadName, direction)
                .WithGeometry(geometry);
        }

        RoadRestrictionFeatureBuilder IRoadRestrictionFeatureBuilderFactory.RoadRestriction(string roadName, Direction direction, LineString geometry)
        {
            return new RoadRestrictionFeatureBuilder(SourceId, FeatureId, roadName, direction)
                .WithGeometry(geometry);
        }

        RoadRestrictionFeatureBuilder IRoadRestrictionFeatureBuilderFactory.RoadRestriction(string roadName, Direction direction, MultiPoint geometry)
        {
            return new RoadRestrictionFeatureBuilder(SourceId, FeatureId, roadName, direction)
                .WithGeometry(geometry);
        }

        ArrowBoardFeatureBuilder IFieldDeviceFeatureBuilderFactory.ArrowBoard(string roadName, Point geometry)
        {
            return new ArrowBoardFeatureBuilder(SourceId, FeatureId, roadName)
                .WithGeometry(geometry);
        }

        CameraFeatureBuilder IFieldDeviceFeatureBuilderFactory.Camera(string roadName, Point geometry)
        {
            return new CameraFeatureBuilder(SourceId, FeatureId, roadName)
                .WithGeometry(geometry);
        }

        DynamicMessageSignFeatureBuilder IFieldDeviceFeatureBuilderFactory.DynamicMessageSign(string roadName, Point geometry)
        {
            return new DynamicMessageSignFeatureBuilder(SourceId, FeatureId, roadName)
                .WithGeometry(geometry);
        }

        FlashingBeaconFeatureBuilder IFieldDeviceFeatureBuilderFactory.FlashingBeacon(string roadName, Point geometry,
            FlashingBeaconFunction function)
        {
            return new FlashingBeaconFeatureBuilder(SourceId, FeatureId, roadName)
                .WithFunction(function)
                .WithGeometry(geometry);
        }

        HybridSignFeatureBuilder IFieldDeviceFeatureBuilderFactory.HybridSign(string roadName, Point geometry,
            HybridSignDynamicMessageFunction function)
        {
            return new HybridSignFeatureBuilder(SourceId, FeatureId, roadName)
                .WithFunction(function)
                .WithGeometry(geometry);
        }

        LocationMarkerFeatureBuilder IFieldDeviceFeatureBuilderFactory.LocationMarker(string roadName, Point geometry,
            Func<MarkedLocationBuilder, MarkedLocationBuilder> locationSetup)
        {
            return new LocationMarkerFeatureBuilder(SourceId, FeatureId, roadName)
                .WithMarkedLocation(locationSetup)
                .WithGeometry(geometry);
        }

        TrafficSensorFeatureBuilder IFieldDeviceFeatureBuilderFactory.TrafficSensor(string roadName, Point geometry)
        {
            return new TrafficSensorFeatureBuilder(SourceId, FeatureId, roadName)
                .WithGeometry(geometry);
        }
    }
}