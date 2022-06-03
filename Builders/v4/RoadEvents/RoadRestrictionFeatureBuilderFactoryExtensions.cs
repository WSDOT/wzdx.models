using Wsdot.Wzdx.GeoJson.Geometries;
using Wsdot.Wzdx.v4.Feeds;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.RoadEvents
{
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