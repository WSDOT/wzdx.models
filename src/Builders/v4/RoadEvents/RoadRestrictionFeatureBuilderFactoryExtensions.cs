using System;
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
        public static RoadRestrictionFeatureBuilder RoadRestriction(this IRoadRestrictionFeatureBuilderFactory factory,
            string roadName, Direction direction, LineString geometry, RestrictionType restrictionType,
            Func<RestrictionBuilder, RestrictionBuilder> restrictionBuilder)
        {
            return new RoadRestrictionFeatureBuilder(factory.SourceId, factory.FeatureId, roadName, direction, restrictionType, restrictionBuilder)
                .WithGeometry(geometry);
        }

        public static RoadRestrictionFeatureBuilder RoadRestriction(this IRoadRestrictionFeatureBuilderFactory factory, string roadName, Direction direction, MultiPoint geometry,
            RestrictionType restrictionType,
            Func<RestrictionBuilder, RestrictionBuilder> restrictionBuilder)
        {
            return new RoadRestrictionFeatureBuilder(factory.SourceId, factory.FeatureId, roadName, direction, restrictionType, restrictionBuilder)
                .WithGeometry(geometry);
        }
    }
}