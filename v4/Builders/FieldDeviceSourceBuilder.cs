using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.v4.Devices;
using Wsdot.Wzdx.v4.Feeds;

namespace Wsdot.Wzdx.v4.Builders
{
    public class FieldDeviceSourceBuilder :
        FeedSourceBuilder<FieldDeviceSourceBuilder>
    {
        private readonly ICollection<Builder<FieldDeviceFeature>> _features;
        private readonly string _id;

        public FieldDeviceSourceBuilder(string id) :
            base(id)
        {
            _id = id;
            _features = new List<Builder<FieldDeviceFeature>>();
        }

        private FieldDeviceSourceBuilder(string id, IEnumerable<Action<FeedDataSource>> configuration, IEnumerable<Builder<FieldDeviceFeature>> features, Action<FeedDataSource> step) :
            base(id, configuration, step)
        {
            _id = id;
            _features = new List<Builder<FieldDeviceFeature>>(features);
        }

        private FieldDeviceSourceBuilder(string id, IEnumerable<Action<FeedDataSource>> configuration, IEnumerable<Builder<FieldDeviceFeature>> features, Builder<FieldDeviceFeature> builder) :
            base(id, configuration)
        {
            _id = id;
            _features = new List<Builder<FieldDeviceFeature>>(features) { builder };
        }

        public FieldDeviceSourceBuilder WithFeature(Builder<FieldDeviceFeature> builder)
        {
            return new FieldDeviceSourceBuilder(_id, Configuration, _features, builder);
        }
        
        public FieldDeviceSourceBuilder WithFeature(string featureId, Func<IFieldDeviceFeatureBuilderFactory, Builder<FieldDeviceFeature>> config)
        {
            return WithFeature(config(new FeatureBuilderFactory(_id, featureId)));
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