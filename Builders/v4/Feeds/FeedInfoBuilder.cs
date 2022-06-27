using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Wsdot.Wzdx.v4.Feeds
{
    /// <summary>
    /// Provides an immutable builder of a v4 FeedInfo (ArrowBoard) class
    /// </summary>
    public class FeedInfoBuilder
    {
        private readonly ICollection<Action<FeedInfo>> _configuration;
        
        public FeedInfoBuilder(string publisher) : this(publisher, new Version(4, 0))
        {

        }

        public FeedInfoBuilder(string publisher, Version version)
        {
            _configuration = new List<Action<FeedInfo>>();
            _configuration.Add(info => info.Publisher = publisher);
            _configuration.Add(info => info.Version = version.ToString(2));
            _configuration.Add(info => info.UpdateFrequency = int.MaxValue);
        }

        private FeedInfoBuilder(IEnumerable<Action<FeedInfo>> configuration, Action<FeedInfo> step)
        {
            _configuration = new List<Action<FeedInfo>>(configuration) { step };
        }

        [Pure]
        public FeedInfoBuilder WithPublisher(string value)
        {
            return new FeedInfoBuilder(_configuration, info => info.Publisher = value);
        }
        
        [Pure]
        public FeedInfoBuilder WithVersion(Version value)
        {
            return new FeedInfoBuilder(_configuration, info => info.Version = value.ToString(2));
        }

        [Pure]
        public FeedInfoBuilder WithUpdateFrequency(TimeSpan value)
        {
            return new FeedInfoBuilder(_configuration, info => info.UpdateFrequency = (int)value.TotalSeconds);
        }

        [Pure]
        public FeedInfoBuilder WithUpdateDate(DateTimeOffset value)
        {
            return new FeedInfoBuilder(_configuration, info => info.UpdateDate = value);
        }

        [Pure]
        public FeedInfoBuilder WithContactName(string value)
        {
            return new FeedInfoBuilder(_configuration, info => info.ContactName = value);
        }

        [Pure]
        public FeedInfoBuilder WithContactEmail(string value)
        {
            return new FeedInfoBuilder(_configuration, info => info.ContactEmail = value);
        }

        [Pure]
        public FeedInfoBuilder WithLicense(LicenseType value)
        {
            return new FeedInfoBuilder(_configuration, info => info.License = value);
        }

        [Pure]
        public FeedInfo Result()
        {
            var result = new FeedInfo();
            foreach (var config in _configuration)
            {
                config(result);
            }

            return result;
        }
    }
}