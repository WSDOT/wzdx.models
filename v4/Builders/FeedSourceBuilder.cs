using System;
using System.Collections.Generic;
using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.v4.Feeds;

namespace Wsdot.Wzdx.v4.Builders
{
    public abstract class FeedSourceBuilder<TFeedSourceBuilder> :
        IBuilder<FeedDataSource>
        where TFeedSourceBuilder : FeedSourceBuilder<TFeedSourceBuilder>
        
    {
        private protected readonly ICollection<Action<FeedDataSource>> _configuration;

        protected FeedSourceBuilder(string id)
        {
            _configuration = new List<Action<FeedDataSource>>();
            _configuration.Add(source => source.DataSourceId = id);
            _configuration.Add(source => source.OrganizationName = id);
            _configuration.Add(source => source.UpdateFrequency = int.MaxValue);
        }

        protected FeedSourceBuilder(string id, IEnumerable<Action<FeedDataSource>> configuration, Action<FeedDataSource> step)
        {
            _configuration = new List<Action<FeedDataSource>>(configuration) { step };
        }

        protected FeedSourceBuilder(string id, IEnumerable<Action<FeedDataSource>> configuration)
        {
            _configuration = new List<Action<FeedDataSource>>(configuration);
        }

        public TFeedSourceBuilder WithOrganizationName(string value)
        {
            return Create(_configuration, source => source.OrganizationName = value);
        }

        public TFeedSourceBuilder WithUpdateFrequency(TimeSpan value)
        {
            return Create(_configuration, info => info.UpdateFrequency = (int)value.TotalSeconds);
        }

        public TFeedSourceBuilder WithUpdateDate(DateTimeOffset value)
        {
            return Create(_configuration, info => info.UpdateDate = value);
        }

        public TFeedSourceBuilder WithContactName(string value)
        {
            return Create(_configuration, info => info.ContactName = value);
        }

        public TFeedSourceBuilder WithContactEmail(string value)
        {
            return Create(_configuration, info => info.ContactEmail = value);
        }

        protected abstract TFeedSourceBuilder Create(ICollection<Action<FeedDataSource>> configuration, Action<FeedDataSource> setup);

        public FeedDataSource Result()
        {
            var result = new FeedDataSource();
            foreach (var config in _configuration)
            {
                config(result);
            }

            return result;
        }
    }
}