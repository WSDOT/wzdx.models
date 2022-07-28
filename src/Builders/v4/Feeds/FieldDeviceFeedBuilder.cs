using System;
using System.Collections.Generic;
using Wzdx.Core;
using Wzdx.v4.Devices;

namespace Wzdx.v4.Feeds
{
    public class FieldDeviceFeedBuilder : IBuilder<FieldDeviceFeed>
    {
        private readonly ICollection<FieldDeviceFeature> _features = new List<FieldDeviceFeature>();
        private FeedInfoBuilder _infoBuilder;

        private FieldDeviceFeedBuilder(string publisher)
        {
            _infoBuilder = new FeedInfoBuilder(publisher);
        }
        
        public FieldDeviceFeedBuilder WithInfo(Func<FeedInfoBuilder, FeedInfoBuilder> setup)
        {
            _infoBuilder = setup(_infoBuilder);
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
            var source = setup(new FieldDeviceSourceBuilder(sourceId)).Result(out var features);
            foreach (var feature in features)
            {
                _features.Add(feature);
            }

            _infoBuilder.WithSource(sourceId, sourceBuilder => sourceBuilder.From(source));

            return this;
        }

        public FieldDeviceFeed Result()
        {
            // todo: determine feed info / source update date?

            //    feedInfo.UpdateDate = source.UpdateDate.Value;
            //if (source.UpdateDate.HasValue && feedInfo.UpdateDate < source.UpdateDate.Value)
            // match feed update date to max source update date 

            // match source update date to max item update date 
            //if (source.UpdateDate < feature.Properties.CoreDetails.UpdateDate)
            //    source.UpdateDate = feature.Properties.CoreDetails.UpdateDate;

            return new FieldDeviceFeed
            {
                FeedInfo = _infoBuilder.Result(),
                Features = _features,
                // todo: determine feed bbox?
            };
        }
        public static IFactory<FieldDeviceFeedBuilder> Factory(string publisher)
        {
            return new DelegatingFactory<FieldDeviceFeedBuilder>(() => new FieldDeviceFeedBuilder(publisher));
        }
    }
}