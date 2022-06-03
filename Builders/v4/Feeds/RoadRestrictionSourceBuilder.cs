using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.v4.RoadEvents;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.Feeds
{
    public sealed class RoadRestrictionSourceBuilder :
        FeedSourceBuilder<RoadRestrictionSourceBuilder>
    {
        private readonly ICollection<RoadRestrictionFeatureBuilder> _features;
        private readonly string _id;

        public RoadRestrictionSourceBuilder(string id) :
            base(id)
        {
            _id = id;
            _features = new List<RoadRestrictionFeatureBuilder>();
        }

        private RoadRestrictionSourceBuilder(string id, IEnumerable<Action<FeedDataSource>> configuration, IEnumerable<RoadRestrictionFeatureBuilder> features, Action<FeedDataSource> step) :
            base(id, configuration, step)
        {
            _id = id;
            _features = new List<RoadRestrictionFeatureBuilder>(features);
        }

        private RoadRestrictionSourceBuilder(string id, IEnumerable<Action<FeedDataSource>> configuration, IEnumerable<RoadRestrictionFeatureBuilder> features, RoadRestrictionFeatureBuilder builder) :
            base(id, configuration)
        {
            _id = id;
            _features = new List<RoadRestrictionFeatureBuilder>(features) { builder };
        }

        public RoadRestrictionSourceBuilder WithFeature(RoadRestrictionFeatureBuilder builder)
        {
            return new RoadRestrictionSourceBuilder(_id, Configuration, _features, builder);
        }

        public RoadRestrictionSourceBuilder WithFeature(string featureId, Func<IRoadRestrictionFeatureBuilderFactory, RoadRestrictionFeatureBuilder> config)
        {
            return WithFeature(config(new FeatureBuilderFactory(_id, featureId)));
        }

        protected override RoadRestrictionSourceBuilder Create(ICollection<Action<FeedDataSource>> configuration, Action<FeedDataSource> setup)
        {
            return new RoadRestrictionSourceBuilder(_id, configuration, _features, setup);
        }

        public IEnumerable<RoadEventFeature> Features()
        {
            return _features.Select(builder => builder.Result());
        }
    }
}