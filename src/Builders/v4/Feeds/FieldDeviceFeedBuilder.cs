using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.Core;

namespace Wsdot.Wzdx.v4.Feeds
{
    public class FieldDeviceFeedBuilder : IBuilder<FieldDeviceFeed>
    {
        private FeedInfoBuilder _infoBuilder;
        private readonly ICollection<FieldDeviceSourceBuilder> _sourcesBuilders = new List<FieldDeviceSourceBuilder>();

        private FieldDeviceFeedBuilder(string publisher)
        {
            _infoBuilder = new FeedInfoBuilder(publisher);
        }

        public static IFactory<FieldDeviceFeedBuilder> Factory(string publisher)
        {
            return new DelegatingFactory<FieldDeviceFeedBuilder>(() => new FieldDeviceFeedBuilder(publisher));
        }

        public FieldDeviceFeedBuilder WithInfo(Func<FeedInfoBuilder, FeedInfoBuilder> setupAction)
        {
            _infoBuilder = setupAction(_infoBuilder);
            return this;
        }

        public FieldDeviceFeedBuilder WithPublisher(string value)
        {
            WithInfo(builder => builder.WithPublisher(value));
            return this;
        }

        public FieldDeviceFeedBuilder WithVersion(Version value)
        {
            WithInfo(builder => builder.WithVersion(value));
            return this;
        }

        public FieldDeviceFeedBuilder WithUpdateFrequency(TimeSpan value)
        {
            WithInfo(builder => builder.WithUpdateFrequency(value));
            return this;
        }

        public FieldDeviceFeedBuilder WithUpdateDate(DateTimeOffset value)
        {
            WithInfo(builder => builder.WithUpdateDate(value));
            return this;
        }

        public FieldDeviceFeedBuilder WithSource(string sourceId, Func<FieldDeviceSourceBuilder, FieldDeviceSourceBuilder> setup)
        {
            var builder = setup(new FieldDeviceSourceBuilder(sourceId));
            _sourcesBuilders.Add(builder);

            return this;
        }

        public FieldDeviceFeed Result()
        {
            var feedInfo = _infoBuilder.Result();

            // add default publisher source if non are defined
            if (!_sourcesBuilders.Any())
                _sourcesBuilders.Add(new FieldDeviceSourceBuilder(feedInfo.Publisher));

            var result = new FieldDeviceFeed();
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
                if (source.UpdateDate.HasValue && feedInfo.UpdateDate < source.UpdateDate.Value)
                    feedInfo.UpdateDate = source.UpdateDate.Value;
            }

            result.FeedInfo = feedInfo;
            return result;
        }
    }
}