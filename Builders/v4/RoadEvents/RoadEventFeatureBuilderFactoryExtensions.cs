using Wsdot.Wzdx.GeoJson.Geometries;
using Wsdot.Wzdx.v4.Feeds;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.RoadEvents
{
    public static class RoadEventFeatureBuilderFactoryExtensions
    {

        public static WorkZoneRoadEventFeatureBuilder WorkZone(this IRoadEventFeatureBuilderFactory factory, string roadName, Direction direction, LineString geometry)
        {
            return new WorkZoneRoadEventFeatureBuilder(factory.SourceId, factory.FeatureId, roadName, direction)
                .WithGeometry(geometry);
        }

        public static WorkZoneRoadEventFeatureBuilder WorkZone(this IRoadEventFeatureBuilderFactory factory, string roadName, Direction direction, MultiPoint geometry)
        {
            return new WorkZoneRoadEventFeatureBuilder(factory.SourceId, factory.FeatureId, roadName, direction)
                .WithGeometry(geometry);
        }

        public static DetourRoadEventFeatureBuilder Detour(this IRoadEventFeatureBuilderFactory factory, string roadName, Direction direction, LineString geometry)
        {
            return new DetourRoadEventFeatureBuilder(factory.SourceId, factory.FeatureId, roadName, direction)
                .WithGeometry(geometry);
        }

        public static DetourRoadEventFeatureBuilder Detour(this IRoadEventFeatureBuilderFactory factory, string roadName, Direction direction, MultiPoint geometry)
        {
            return new DetourRoadEventFeatureBuilder(factory.SourceId, factory.FeatureId, roadName, direction)
                .WithGeometry(geometry);
        }
    }
}