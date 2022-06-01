using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.v4.Feeds;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.Builders
{
    public sealed class RoadEventSourceBuilder :
        FeedSourceBuilder<RoadEventSourceBuilder>
    {
        private readonly ICollection<IBuilder<RoadEventFeature>> _features;
        private readonly string _id;

        public RoadEventSourceBuilder(string id) :
            base(id)
        {
            _id = id;
            _features = new List<IBuilder<RoadEventFeature>>();
        }

        private RoadEventSourceBuilder(string id, IEnumerable<Action<FeedDataSource>> configuration, IEnumerable<IBuilder<RoadEventFeature>> features, Action<FeedDataSource> step) :
            base(id, configuration, step)
        {
            _id = id;
            _features = new List<IBuilder<RoadEventFeature>>(features);
        }

        private RoadEventSourceBuilder(string id, IEnumerable<Action<FeedDataSource>> configuration, IEnumerable<IBuilder<RoadEventFeature>> features, IBuilder<RoadEventFeature> builder) :
            base(id, configuration)
        {
            _id = id;
            _features = new List<IBuilder<RoadEventFeature>>(features) { builder };
        }

        public RoadEventSourceBuilder WithFeature(IBuilder<RoadEventFeature> featureBuilder)
        {
            return new RoadEventSourceBuilder(_id, _configuration, _features, featureBuilder);
        }
        
        public RoadEventSourceBuilder WithWorkZoneFeature(string featureId, string roadName, Direction direction, Action<WorkZoneRoadEventFeatureBuilder> setup)
        {
            var builder = new WorkZoneRoadEventFeatureBuilder(_id, featureId, roadName, direction);
            setup(builder);
            return new RoadEventSourceBuilder(_id, _configuration, _features, builder);
        }

        public RoadEventSourceBuilder WithDetourFeature(string featureId, string roadName, Direction direction, Action<DetourRoadEventFeatureBuilder> setup)
        {
            var builder = new DetourRoadEventFeatureBuilder(_id, featureId, roadName, direction);
            setup(builder);
            return WithFeature(builder);
        }

        protected override RoadEventSourceBuilder Create(ICollection<Action<FeedDataSource>> configuration, Action<FeedDataSource> setup)
        {
            return new RoadEventSourceBuilder(_id, configuration, _features, setup);
        }

        public IEnumerable<RoadEventFeature> Features()
        {
            return _features.Select(builder => builder.Result());
        }
    }
}