using System;
using Wzdx.Core;
using Wzdx.v4.Devices;

namespace Wzdx.v4.Feeds
{
    public sealed class FieldDeviceSourceBuilder :
        FeedSourceFeatureBuilder<FieldDeviceSourceBuilder, FieldDeviceFeature, IBuilder<FieldDeviceFeature>>
    {
        public FieldDeviceSourceBuilder(string sourceId) :
            base(sourceId)
        {

        }
        
        public FieldDeviceSourceBuilder WithFeature(string featureId, Func<IFieldDeviceFeatureBuilderFactory, IBuilder<FieldDeviceFeature>> config)
        {
            return WithFeature(config(new FeatureBuilderFactory(SourceId, featureId)));
        }
    }
}