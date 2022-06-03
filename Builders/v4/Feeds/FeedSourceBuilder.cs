using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.Core;

namespace Wsdot.Wzdx.v4.Feeds
{
    public abstract class FeedSourceBuilder<TFeedSourceBuilder> :
        IBuilder<FeedDataSource>
        where TFeedSourceBuilder : FeedSourceBuilder<TFeedSourceBuilder>

    {
        private protected readonly ICollection<Action<FeedDataSource>> Configuration;

        protected FeedSourceBuilder(string id) :
            this(id, new List<Action<FeedDataSource>>())
        {

        }

        protected FeedSourceBuilder(string id, IEnumerable<Action<FeedDataSource>> configuration, Action<FeedDataSource> step) :
            this(id, new List<Action<FeedDataSource>>(configuration) { step })
        {

        }

        protected FeedSourceBuilder(string id, IEnumerable<Action<FeedDataSource>> configuration)
        {
            configuration = configuration.ToList();
            Configuration = configuration.Count() < 3
                ? new Action<FeedDataSource>[]
                    {
                        source => source.DataSourceId = id,
                        source => source.OrganizationName = id,
                        source => source.UpdateFrequency = int.MaxValue
                    }
                    .Union(configuration)
                    .ToList()
                : new List<Action<FeedDataSource>>(configuration);
        }

        /// <summary>
        /// Returns a builder containing configuration with source identifier
        /// </summary>
        /// <returns>Source Builder<typeparam name="TFeedSourceBuilder"></typeparam></returns>
        public TFeedSourceBuilder WithSourceId(string value)
        {
            return Create(Configuration, source => source.DataSourceId = value);
        }

        /// <summary>
        /// Returns a builder containing configuration with organization name
        /// </summary>
        /// <returns>Source Builder<typeparam name="TFeedSourceBuilder"></typeparam></returns>
        public TFeedSourceBuilder WithOrganizationName(string value)
        {
            return Create(Configuration, source => source.OrganizationName = value);
        }

        /// <summary>
        /// Returns a builder containing configuration with contact name
        /// </summary>
        /// <returns>Source Builder<typeparam name="TFeedSourceBuilder"></typeparam></returns>
        public TFeedSourceBuilder WithContactName(string value)
        {
            return Create(Configuration, info => info.ContactName = value);
        }

        /// <summary>
        /// Returns a builder containing configuration with contact email
        /// </summary>
        /// <returns>Source Builder<typeparam name="TFeedSourceBuilder"></typeparam></returns>
        public TFeedSourceBuilder WithContactEmail(string value)
        {
            return Create(Configuration, info => info.ContactEmail = value);
        }

        /// <summary>
        /// Returns a builder containing configuration with contact name and email
        /// </summary>
        /// <returns>Source Builder<typeparam name="TFeedSourceBuilder"></typeparam></returns>
        public TFeedSourceBuilder WithContact(string name, string email)
        {
            return Create(Configuration, source =>
            {
                source.ContactName = name;
                source.ContactEmail = email;
            });
        }

        /// <summary>
        /// Returns a builder containing configuration for no contact information
        /// </summary>
        /// <returns></returns>
        /// <returns>Source Builder<typeparam name="TFeedSourceBuilder"></typeparam></returns>
        public TFeedSourceBuilder WithNoContact()
        {
            return Create(Configuration, source =>
            {
                source.ContactName = null;
                source.ContactEmail = null;
            });
        }

        /// <summary>
        /// Returns a builder containing configuration for frequency source is updated in total seconds
        /// </summary>
        /// <param name="value">TimeSpan value representing frequency of updates</param>
        /// <returns>Source Builder<typeparam name="TFeedSourceBuilder"></typeparam></returns>
        public TFeedSourceBuilder WithUpdateFrequency(TimeSpan value)
        {
            return Create(Configuration, info => info.UpdateFrequency = (int)value.TotalSeconds);
        }

        /// <summary>
        /// Returns a builder containing configuration for the last updated date time (UTC)
        /// </summary>
        /// <param name="value">DateTimeOffset containing the last updated date time in UTC</param>
        /// <returns>Source Builder<typeparam name="TFeedSourceBuilder"></typeparam></returns>
        public TFeedSourceBuilder WithUpdateDate(DateTimeOffset value)
        {
            return Create(Configuration, info => info.UpdateDate = value.UtcDateTime);
        }

        protected abstract TFeedSourceBuilder Create(ICollection<Action<FeedDataSource>> configuration, Action<FeedDataSource> setup);

        /// <summary>
        /// Returns the completed feed data source as configured by the builder
        /// </summary>
        /// <returns></returns>
        public FeedDataSource Result()
        {
            var result = new FeedDataSource();
            foreach (var config in Configuration)
            {
                config(result);
            }

            return result;
        }
    }
}