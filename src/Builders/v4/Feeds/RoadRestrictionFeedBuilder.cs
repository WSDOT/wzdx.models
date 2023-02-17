using System;
using System.Collections.Generic;
using Wzdx.Core;
using Wzdx.v4.WorkZones;

namespace Wzdx.v4.Feeds
{
    public class RoadRestrictionFeedBuilder : IBuilder<RoadRestrictionFeed>
    {
        private readonly ICollection<RoadEventFeature> _features = new List<RoadEventFeature>();
        private FeedInfoBuilder _infoBuilder;
        
        private RoadRestrictionFeedBuilder(string publisher)
        {
            _infoBuilder = new FeedInfoBuilder(publisher);
        }

        public static IFactory<RoadRestrictionFeedBuilder> Factory(string publisher)
        {
            return new DelegatingFactory<RoadRestrictionFeedBuilder>(() => new RoadRestrictionFeedBuilder(publisher));
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

        public RoadRestrictionFeedBuilder WithUpdateFrequency(TimeSpan? value)
        {
            WithInfo(builder => value.HasValue ? builder.WithUpdateFrequency(value.Value) : builder.WithNoUpdateFrequency());
            return this;
        }
        
        public RoadRestrictionFeedBuilder WithNoUpdateFrequency()
        {
            WithInfo(builder => builder.WithNoUpdateFrequency());
            return this;
        }

        public RoadRestrictionFeedBuilder WithUpdateDate(DateTimeOffset value)
        {
            WithInfo(builder => builder.WithUpdateDate(value));
            return this;
        }
        
        public RoadRestrictionFeedBuilder WithSource(string sourceId, Func<RoadRestrictionSourceBuilder, RoadRestrictionSourceBuilder> setup)
        {
            var source = setup(new RoadRestrictionSourceBuilder(sourceId)).Result(out var features);
            foreach (var feature in features)
            {
                _features.Add(feature); 
                if (source.UpdateDate < feature.Properties.CoreDetails.UpdateDate)
                    source.UpdateDate = feature.Properties.CoreDetails.UpdateDate;
            }

            _infoBuilder.WithSource(sourceId, sourceBuilder => sourceBuilder.From(source));

            return this;
        }

        public RoadRestrictionFeed Result()
        {
            return new RoadRestrictionFeed
            {
                FeedInfo = _infoBuilder.Result(),
                Features = _features,
                // todo: determine feed bbox?
            };
        }
    }
}