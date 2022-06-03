using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.GeoJson.Geometries;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.Builders
{
    public class RoadRestrictionFeatureBuilder : RoadEventFeatureBuilder<RoadRestrictionFeatureBuilder, RestrictionRoadEvent>
    {
        
        public RoadRestrictionFeatureBuilder(string sourceId, string featureId, string roadName, Direction direction) : 
            this(new List<Action<RoadEventFeature>>(), (feature, restriction) =>
            {
                var geometry = MultiPoint.FromCoordinates(Enumerable.Empty<Position>());
                feature.Id = featureId;
                feature.Geometry = geometry;
                feature.BoundaryBox = geometry.BoundaryBox.ToList().AsReadOnly();
                feature.Properties.CoreDetails.DataSourceId = sourceId;
                feature.Properties.CoreDetails.RoadNames.Add(roadName);
                feature.Properties.CoreDetails.Direction = direction;
            })
        {
            
        }

        private RoadRestrictionFeatureBuilder(IEnumerable<Action<RoadEventFeature>> configuration, Action<RoadEventFeature, RestrictionRoadEvent> step) : 
            base(configuration, step)
        {
            // ignore
        }


        // ReSharper disable once UnusedMember.Global
        public RoadRestrictionFeatureBuilder WithLane(LaneType type, LaneStatus status, int order, Action<LaneBuilder> configure)
        {
            var builder = new LaneBuilder(type, status, order);
            configure(builder);
            var lane = builder.Result();
            return CreateWith((_, restriction) => restriction.Lanes.Add(lane));
        }

        // ReSharper disable once UnusedMember.Global
        public RoadRestrictionFeatureBuilder WithRestriction(RestrictionType type, UnitOfMeasurement unit, Action<RestrictionBuilder> configure)
        {
            var builder = new RestrictionBuilder(type, unit);
            configure(builder);
            var lane = builder.Result();
            return CreateWith((_, restriction) => restriction.Restrictions.Add(lane));
        }

        protected override Func<RoadEventFeature> ResultFactory { get; } = () =>
            new RoadEventFeature()
            {
                Properties = new RestrictionRoadEvent() { }
            };

        
        protected override RoadRestrictionFeatureBuilder CreateWith(Action<RoadEventFeature, RestrictionRoadEvent> step)
        {
            return new RoadRestrictionFeatureBuilder(Configuration, step);
        }

        protected override Func<RestrictionRoadEvent> ResultProperties { get; } = () =>
            new RestrictionRoadEvent();
    }
}