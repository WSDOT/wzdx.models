using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.v4.Feeds;

namespace Wsdot.Wzdx.v4.Builders
{
    public class RoadRestrictionFeedBuilder : IBuilder<RoadRestrictionFeed>
    {
        private FeedInfoBuilder _infoBuilder;
        private readonly ICollection<RoadRestrictionSourceBuilder> _sourcesBuilders = new List<RoadRestrictionSourceBuilder>();

        private RoadRestrictionFeedBuilder(string publisher)
        {
            _infoBuilder = new FeedInfoBuilder(publisher);
        }

        public static IFactory<RoadRestrictionFeedBuilder> Factory(string publisher)
        {
            return new RoadRestrictionFeedBuilderFactory(publisher);
        }

        public RoadRestrictionFeedBuilder WithInfo(Func<FeedInfoBuilder, FeedInfoBuilder> setupAction)
        {
            _infoBuilder = setupAction(_infoBuilder);
            return this;
        }

        public RoadRestrictionFeedBuilder WithPublisher(string value)
        {
            WithInfo(builder => builder.WithPublisher(value));
            return this;
        }

        public RoadRestrictionFeedBuilder WithVersion(Version value)
        {
            WithInfo(builder => builder.WithVersion(value));
            return this;
        }

        public RoadRestrictionFeedBuilder WithUpdateFrequency(TimeSpan value)
        {
            WithInfo(builder => builder.WithUpdateFrequency(value));
            return this;
        }

        public RoadRestrictionFeedBuilder WithUpdateDate(DateTimeOffset value)
        {
            WithInfo(builder => builder.WithUpdateDate(value));
            return this;
        }

        public RoadRestrictionFeedBuilder WithSource(string sourceId, Func<RoadRestrictionSourceBuilder, RoadRestrictionSourceBuilder> setup)
        {
            var builder = setup(new RoadRestrictionSourceBuilder(sourceId));
            _sourcesBuilders.Add(builder);

            return this;
        }

        public RoadRestrictionFeed Result()
        {
            var feedInfo = _infoBuilder.Result();

            // add default publisher source if non are defined
            if (!_sourcesBuilders.Any())
                _sourcesBuilders.Add(new RoadRestrictionSourceBuilder(feedInfo.Publisher));

            var result = new RoadRestrictionFeed();
            foreach (var sourceBuilder in _sourcesBuilders)
            {
                var source = sourceBuilder.Result();
                feedInfo.DataSources.Add(source);

                foreach (var feature in sourceBuilder.Features())
                {
                    // match source update date to max item update date 
                    if (source.UpdateDate < feature.Properties.CoreDetails.UpdateDate)
                        source.UpdateDate = feature.Properties.CoreDetails.UpdateDate;

                    result.Features.Add(feature);
                }

                // match feed update date to max source update date 
                if (feedInfo.UpdateDate < source.UpdateDate)
                    feedInfo.UpdateDate = source.UpdateDate;
            }

            result.FeedInfo = feedInfo;
            return result;
        }

        // todo: review is builder factory needed ??
        private class RoadRestrictionFeedBuilderFactory : IFactory<RoadRestrictionFeedBuilder>
        {
            private readonly string _publisher;

            public RoadRestrictionFeedBuilderFactory(string publisher)
            {
                _publisher = publisher;
            }

            public RoadRestrictionFeedBuilder Create()
            {
                return new RoadRestrictionFeedBuilder(_publisher);
            }
        }
    }
}