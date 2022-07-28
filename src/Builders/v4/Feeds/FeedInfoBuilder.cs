using System;
using System.Diagnostics.Contracts;
using Wzdx.Core;

namespace Wzdx.v4.Feeds
{
    /// <summary>
    /// Provides a builder for a v4 FeedInfo class
    /// </summary>
    public sealed class FeedInfoBuilder : IBuilder<FeedInfo>
    {
        private readonly BuilderConfiguration<FeedInfo> _configuration =
            new BuilderConfiguration<FeedInfo>();

        public FeedInfoBuilder(string publisher) : this(publisher, new Version(4, 0))
        {

        }

        public FeedInfoBuilder(string publisher, Version version)
        {
            WithPublisher(publisher);
            WithVersion(version);
        }

        public FeedInfoBuilder WithPublisher(string value)
        {
            _configuration.Set(info => info.Publisher, value);
            return this;
        }

        public FeedInfoBuilder WithSource(string sourceId)
        {
            return WithSource(sourceId, _ => _);
        }

        public FeedInfoBuilder WithSource(string sourceId, Func<FeedSourceBuilder, FeedSourceBuilder> setup)
        {
            var source = setup(new FeedSourceBuilder(sourceId)).Result();
            _configuration.Combine(info => info.DataSources, info => info.DataSources.Add(source));
            return this;
        }

        public FeedInfoBuilder WithVersion(Version value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            _configuration.Set(info => info.Version, value.ToString(2));
            return this;
        }

        public FeedInfoBuilder WithUpdateFrequency(TimeSpan value)
        {
            _configuration.Set(info => info.UpdateFrequency, (int)value.TotalSeconds);
            return this;
        }

        public FeedInfoBuilder WithNoUpdateFrequency()
        {
            _configuration.Set(info => info.UpdateFrequency, (int?)null);
            return this;
        }

        public FeedInfoBuilder WithUpdateDate(DateTimeOffset value)
        {
            _configuration.Set(info => info.UpdateDate, value);
            return this;
        }

        public FeedInfoBuilder WithContact(string name, string email)
        {
            return WithContactName(name).WithContactEmail(email);
        }

        public FeedInfoBuilder WithNoContact()
        {
            return WithContactName(null).WithContactEmail(null);
        }

        public FeedInfoBuilder WithContactName(string value)
        {

            _configuration.Set(info => info.ContactName, value);
            return this;
        }

        public FeedInfoBuilder WithContactEmail(string value)
        {
            _configuration.Set(info => info.ContactEmail, value);
            return this;
        }


        public FeedInfoBuilder WithLicense(LicenseType value)
        {
            _configuration.Set(info => info.License, value);
            return this;
        }

        public FeedInfoBuilder WithNoLicense()
        {
            _configuration.Default(info => info.License);
            return this;
        }

        [Pure]
        public FeedInfo Result()
        {
            var result = new FeedInfo();
            _configuration.ApplyTo(result);

            if (result.DataSources.Count == 0)
                result.DataSources.Add(new FeedSourceBuilder(result.Publisher).Result());

            return result;
        }

        
    }
}