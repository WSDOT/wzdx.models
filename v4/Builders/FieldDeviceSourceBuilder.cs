using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.v4.Devices;
using Wsdot.Wzdx.v4.Feeds;

namespace Wsdot.Wzdx.v4.Builders
{
    public class FieldDeviceSourceBuilder :
        FeedSourceBuilder<FieldDeviceSourceBuilder>
    {
        private readonly ICollection<FieldDeviceFeatureBuilder> _features;
        private readonly string _id;

        public FieldDeviceSourceBuilder(string id) :
            base(id)
        {
            _id = id;
            _features = new List<FieldDeviceFeatureBuilder>();
        }

        private FieldDeviceSourceBuilder(string id, IEnumerable<Action<FeedDataSource>> configuration, IEnumerable<FieldDeviceFeatureBuilder> features, Action<FeedDataSource> step) :
            base(id, configuration, step)
        {
            _id = id;
            _features = new List<FieldDeviceFeatureBuilder>(features);
        }

        private FieldDeviceSourceBuilder(string id, IEnumerable<Action<FeedDataSource>> configuration, IEnumerable<FieldDeviceFeatureBuilder> features, FieldDeviceFeatureBuilder builder) :
            base(id, configuration)
        {
            _id = id;
            _features = new List<FieldDeviceFeatureBuilder>(features) { builder };
        }

        public FieldDeviceSourceBuilder WithFeature(FieldDeviceFeatureBuilder builder)
        {
            return new FieldDeviceSourceBuilder(_id, Configuration, _features, builder);
        }

        public FieldDeviceSourceBuilder WithArrowBoardFeature(string featureId, string roadName, Func<ArrowBoardFeatureBuilder, ArrowBoardFeatureBuilder> config)
        {
            return WithFeature(config(new ArrowBoardFeatureBuilder(_id, featureId, roadName)));
        }
        
        protected override FieldDeviceSourceBuilder Create(ICollection<Action<FeedDataSource>> configuration, Action<FeedDataSource> setup)
        {
            return new FieldDeviceSourceBuilder(_id, configuration, _features, setup);
        }

        public IEnumerable<FieldDeviceFeature> Features()
        {
            return _features.Select(builder => builder.Result());
        }
    }
}