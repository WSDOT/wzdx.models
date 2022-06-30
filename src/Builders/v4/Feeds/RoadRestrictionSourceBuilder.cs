using System;
using Wsdot.Wzdx.v4.RoadEvents;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.Feeds
{
    /// <summary>
    /// Provides an of a v4 FeedSource class of a Road Restriction Feed 
    /// </summary>
    public sealed class RoadRestrictionSourceBuilder :
        FeedSourceFeatureBuilder<RoadRestrictionSourceBuilder, RoadEventFeature, RoadRestrictionFeatureBuilder>
    {
        public RoadRestrictionSourceBuilder(string sourceId) : 
            base(sourceId)
        {

        }

        public RoadRestrictionSourceBuilder WithFeature(string featureId, Func<IRoadRestrictionFeatureBuilderFactory, RoadRestrictionFeatureBuilder> config)
        {
            return WithFeature(config(new FeatureBuilderFactory(SourceId, featureId)));
        }
    }
}