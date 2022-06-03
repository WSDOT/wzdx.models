using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.Feeds
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
            return new RoadEventSourceBuilder(_id, Configuration, _features, featureBuilder);
        }

        public RoadEventSourceBuilder WithFeature(string featureId, Func<IRoadEventFeatureBuilderFactory, IBuilder<RoadEventFeature>> setup)
        {
            return WithFeature(setup(new FeatureBuilderFactory(_id, featureId)));
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