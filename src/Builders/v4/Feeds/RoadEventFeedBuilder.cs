using System;
using System.Collections.Generic;
using Wzdx.Core;
using Wzdx.v4.WorkZones;

namespace Wzdx.v4.Feeds
{
    public class RoadEventFeedBuilder : IBuilder<RoadEventsFeed>
    {
        private readonly ICollection<RoadEventFeature> _features = new List<RoadEventFeature>();
        private FeedInfoBuilder _infoBuilder;

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

        public RoadEventFeedBuilder WithUpdateFrequency(TimeSpan? value)
        {
            WithInfo(builder => value.HasValue
                ? builder.WithUpdateFrequency(value.Value)
                : builder.WithNoUpdateFrequency());
            return this;
        }

        public RoadEventFeedBuilder WithNoUpdateFrequency()
        {
            WithInfo(builder => builder.WithNoUpdateFrequency());
            return this;
        }

        public RoadEventFeedBuilder WithUpdateDate(DateTimeOffset value)
        {
            WithInfo(builder => builder.WithUpdateDate(value));
            return this;
        }

        public RoadEventFeedBuilder WithSource(string sourceId, Func<RoadEventSourceBuilder, RoadEventSourceBuilder> setup)
        {
            var source = setup(new RoadEventSourceBuilder(sourceId)).Result(out var features);
            foreach (var feature in features)
            {
                _features.Add(feature);
                if (source.UpdateDate < feature.Properties.CoreDetails.UpdateDate)
                    source.UpdateDate = feature.Properties.CoreDetails.UpdateDate;
            }

            _infoBuilder.WithSource(sourceId, sourceBuilder => sourceBuilder.From(source));

            return this;
        }

        public RoadEventsFeed Result()
        {
            return new RoadEventsFeed
            {
                FeedInfo = _infoBuilder.Result(),
                Features = _features,
                // todo: determine feed bbox?
            };
        }
        
        public static IFactory<RoadEventFeedBuilder> Factory(string publisher)
        {
            return new DelegatingFactory<RoadEventFeedBuilder>(() => new RoadEventFeedBuilder(publisher));
        }
    }
}