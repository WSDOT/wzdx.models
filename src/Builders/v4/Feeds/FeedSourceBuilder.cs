using System;
using System.Diagnostics.Contracts;
using Wsdot.Wzdx.Core;

namespace Wsdot.Wzdx.v4.Feeds
{
    public class FeedSourceBuilder : FeedSourceBuilder<FeedSourceBuilder>
    {
        public FeedSourceBuilder(string sourceId) :
            base(sourceId)
        {

        }

        public FeedSourceBuilder From(FeedDataSource source)
        {
            var sourceBuilder = this;
            // assuming immutability
            sourceBuilder = sourceBuilder.WithOrganizationName(source.OrganizationName);
            sourceBuilder = source.UpdateDate.HasValue
                ? sourceBuilder.WithUpdateDate(source.UpdateDate.Value)
                : sourceBuilder.WithNoUpdateDate();
            sourceBuilder = string.IsNullOrEmpty(source.ContactName) && string.IsNullOrEmpty(source.ContactEmail)
                ? sourceBuilder.WithNoContact()
                : sourceBuilder.WithContact(source.ContactName, source.ContactEmail);
            sourceBuilder = source.UpdateFrequency.HasValue
                ? sourceBuilder.WithUpdateFrequency(TimeSpan.FromSeconds(source.UpdateFrequency.Value))
                : sourceBuilder.WithNoUpdateFrequency();
            // ignored source.LocationVerifyMethod
            sourceBuilder = source.LrsUrl == null && string.IsNullOrEmpty(source.LrsType)
                ? sourceBuilder.WithNoLrsInfo()
                : sourceBuilder.WithLrsInfo(source.LrsType, source.LrsUrl);

            return sourceBuilder;
        }
    }

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

        protected FeedSourceBuilder(string sourceId)
        {
            SourceId = sourceId;
            Configuration.Set(source => source.DataSourceId, sourceId);
            Configuration.Set(source => source.OrganizationName, sourceId);
            Configuration.Default(source => source.UpdateFrequency);
        }

        /// <summary>
        /// Returns a builder containing configuration with organization name
        /// </summary>
        public T WithOrganizationName(string value)
        {
            Configuration.Set(info => info.OrganizationName, value);
            return Derived();
        }

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
            // todo: validate email with same schema "format" (email) pattern
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
        public T WithNoUpdateDate()
        {
            return WithUpdateDate(null);
        }

        /// <summary>
        /// Returns a builder containing configuration for lrs information
        /// </summary>
        /// <param name="lrsType"></param>
        /// <param name="lrsUrl"></param>
        public T WithLrsInfo(string lrsType, Uri lrsUrl)
        {
            Configuration.Set(info => info.LrsType, lrsType);
            Configuration.Set(info => info.LrsUrl, lrsUrl);
            return Derived();
        }

        /// <summary>
        /// Returns a builder containing configuration for no lrs information
        /// </summary>
        public T WithNoLrsInfo()
        {
            WithLrsInfo(null, null);
            return Derived();
        }

        [Pure]
        public FeedDataSource Result()
        {
            var result = new FeedDataSource();
            Configuration.ApplyTo(result);
            return result;
        }

        protected T Derived() => (T)this;
    }
}