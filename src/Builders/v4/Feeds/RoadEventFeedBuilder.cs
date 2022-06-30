using System;
using System.Collections.Generic;
using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.Feeds
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
            var source = setup(new RoadEventSourceBuilder(sourceId)).Result(out var features);
            foreach (var feature in features)
            {
                _features.Add(feature);
            }

            _infoBuilder.WithSource(sourceId, sourceBuilder => sourceBuilder.From(source));

            return this;
        }
        
        public RoadEventsFeed Result()
        {
            // todo: determine feed info / source update date?

            //    feedInfo.UpdateDate = source.UpdateDate.Value;
            //if (source.UpdateDate.HasValue && feedInfo.UpdateDate < source.UpdateDate.Value)
            // match feed update date to max source update date 

            // match source update date to max item update date 
            //if (source.UpdateDate < feature.Properties.CoreDetails.UpdateDate)
            //    source.UpdateDate = feature.Properties.CoreDetails.UpdateDate;

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