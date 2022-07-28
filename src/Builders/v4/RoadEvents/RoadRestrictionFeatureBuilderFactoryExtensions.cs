using Wzdx.GeoJson.Geometries;
using Wzdx.v4.Feeds;
using Wzdx.v4.WorkZones;

namespace Wzdx.v4.RoadEvents
{
    /// <summary>
    /// Provides extensions of v4 IRoadRestrictionFeatureBuilderFactory 
    /// </summary>
    public static class RoadRestrictionFeatureBuilderFactoryExtensions
    {
        public static RoadRestrictionFeatureBuilder RoadRestriction(this IRoadRestrictionFeatureBuilderFactory factory, string roadName, Direction direction, LineString geometry)
        {
            return new RoadRestrictionFeatureBuilder(factory.SourceId, factory.FeatureId, roadName, direction)
                .WithGeometry(geometry);
        }

        public static RoadRestrictionFeatureBuilder RoadRestriction(this IRoadRestrictionFeatureBuilderFactory factory, string roadName, Direction direction, MultiPoint geometry)
        {
            return new RoadRestrictionFeatureBuilder(factory.SourceId, factory.FeatureId, roadName, direction)
                .WithGeometry(geometry);
        }
    }
}