using System;
using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.Feeds
{
    public sealed class RoadEventSourceBuilder :
        FeedSourceFeatureBuilder<RoadEventSourceBuilder, RoadEventFeature, IBuilder<RoadEventFeature>>
    {
        public RoadEventSourceBuilder(string sourceId) :
            base(sourceId)
        {

        }
        
        public RoadEventSourceBuilder WithFeature(string featureId, Func<IRoadEventFeatureBuilderFactory, IBuilder<RoadEventFeature>> setup)
        {
            return WithFeature(setup(new FeatureBuilderFactory(SourceId, featureId)));
        }
    }
}