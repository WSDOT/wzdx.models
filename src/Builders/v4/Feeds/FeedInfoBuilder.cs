using System;
using System.Diagnostics.Contracts;
using Wsdot.Wzdx.Core;

namespace Wsdot.Wzdx.v4.Feeds
{
    /// <summary>
    /// Provides an of a v4 FeedInfo (ArrowBoard) class
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

        public FeedInfoBuilder WithVersion(Version value)
        {
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

        [Pure]
        public FeedInfo Result()
        {
            var result = new FeedInfo();
            _configuration.ApplyTo(result);
            return result;
        }
    }
}