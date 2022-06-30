using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.Core;

namespace Wsdot.Wzdx.v4.Feeds
{
    public class RoadEventFeedBuilder : IBuilder<RoadEventsFeed>
    {
        private FeedInfoBuilder _infoBuilder;
        private readonly ICollection<RoadEventSourceBuilder> _sourcesBuilders = new List<RoadEventSourceBuilder>();

        public RoadEventFeedBuilder(string publisher)
        {
            _infoBuilder = new FeedInfoBuilder(publisher);
        }
        public RoadEventFeedBuilder WithInfo(Func<FeedInfoBuilder, FeedInfoBuilder> setupAction)
        {
            _infoBuilder = setupAction(_infoBuilder);
            return this;
        }

        public RoadEventFeedBuilder WithPublisher(string value)
        {
            WithInfo(builder => builder.WithPublisher(value));
            return this;
        }

        public RoadEventFeedBuilder WithVersion(Version value)
        {
            WithInfo(builder => builder.WithVersion(value));
            return this;
        }

        public RoadEventFeedBuilder WithUpdateFrequency(TimeSpan value)
        {
            WithInfo(builder => builder.WithUpdateFrequency(value));
            return this;
        }

        public RoadEventFeedBuilder WithUpdateDate(DateTimeOffset value)
        {
            WithInfo(builder => builder.WithUpdateDate(value));
            return this;
        }

        public RoadEventFeedBuilder WithSource(string sourceId, Func<RoadEventSourceBuilder, RoadEventSourceBuilder> setup)
        {
            var builder = setup(new RoadEventSourceBuilder(sourceId));
            return WithSource(builder);
        }

        public RoadEventFeedBuilder WithSource(RoadEventSourceBuilder builder)
        {
            _sourcesBuilders.Add(builder);
            return this;
        }

        public RoadEventsFeed Result()
        {
            var feedInfo = _infoBuilder.Result();

            // add default publisher source if non are defined
            if (!_sourcesBuilders.Any())
                _sourcesBuilders.Add(new RoadEventSourceBuilder(feedInfo.Publisher));

            var result = new RoadEventsFeed();
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
                if (source.UpdateDate.HasValue && feedInfo.UpdateDate < source.UpdateDate)
                    feedInfo.UpdateDate = source.UpdateDate.Value;
            }

            result.FeedInfo = feedInfo;
            return result;
        }


        public static IFactory<RoadEventFeedBuilder> Factory(string publisher)
        {
            return new DelegatingFactory<RoadEventFeedBuilder>(() => new RoadEventFeedBuilder(publisher));
        }
    }
}