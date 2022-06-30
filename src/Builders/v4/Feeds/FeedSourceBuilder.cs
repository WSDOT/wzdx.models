using System;
using System.Diagnostics.Contracts;
using Wsdot.Wzdx.Core;

namespace Wsdot.Wzdx.v4.Feeds
{
    /// <summary>
    /// Provides an abstract of a v4 FeedDataSource (abstract) class
    /// </summary>
    public abstract class FeedSourceBuilder<T> :
        IBuilder<FeedDataSource>
        where T : FeedSourceBuilder<T>
    {
        protected string SourceId { get; }
        protected BuilderConfiguration<FeedDataSource> Configuration { get; }
            = new BuilderConfiguration<FeedDataSource>();

        protected FeedSourceBuilder(string id)
        {
            SourceId = id;
            Configuration.Set(source => source.DataSourceId, id);
            Configuration.Set(source => source.OrganizationName, id);
            Configuration.Set(source => source.UpdateFrequency, 1); // todo set UpdateFrequency to null value
        }

        /// <summary>
        /// Returns a builder containing configuration with organization name
        /// </summary>
        public T WithOrganizationName(string value)
        {
            Configuration.Set(info => info.OrganizationName, value);
            return Derived();
        }

        protected T Derived() => (T)this;

        /// <summary>
        /// Returns a builder containing configuration with contact name
        /// </summary>
        public T WithContactName(string value)
        {
            Configuration.Set(info => info.ContactName, value);
            return Derived();
        }

        /// <summary>
        /// Returns a builder containing configuration with contact email
        /// </summary>
        public T WithContactEmail(string value)
        {
            Configuration.Set(info => info.ContactEmail, value);
            return Derived();
        }

        /// <summary>
        /// Returns a builder containing configuration with contact name and email
        /// </summary>
        public T WithContact(string name, string email)
        {
            return WithContactName(name).WithContactEmail(email);
        }

        /// <summary>
        /// Returns a builder containing configuration for no contact information
        /// </summary>
        public T WithNoContact()
        {
            return WithContact(null, null);
        }

        /// <summary>
        /// Returns a builder containing configuration for frequency source is updated in total seconds
        /// </summary>
        /// <param name="value">TimeSpan value representing frequency of updates</param>
        public T WithUpdateFrequency(TimeSpan? value)
        {
            Configuration.Set(info => info.UpdateFrequency, (int?)value?.TotalSeconds);
            return Derived();
        }

        public T WithNoUpdateFrequency()
        {
            return WithUpdateFrequency(null);
        }
        /// <summary>
        /// Returns a builder containing configuration for the last updated date time (UTC)
        /// </summary>
        /// <param name="value">DateTimeOffset containing the last updated date time in UTC</param>
        public T WithUpdateDate(DateTimeOffset? value)
        {
            Configuration.Set(info => info.UpdateDate, value?.ToUniversalTime());
            return Derived();
        }
        
        /// <summary>
        /// Returns a builder containing configuration for no last updated date time (UTC)
        /// </summary>
        /// <param name="value">DateTimeOffset containing the last updated date time in UTC</param>
        public T WithNoUpdateDate()
        {
            return WithUpdateDate(null);
        }

        [Pure]
        public FeedDataSource Result()
        {
            var result = new FeedDataSource();
            Configuration.ApplyTo(result);
            return result;
        }
    }
}