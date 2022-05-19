using System;
using System.Collections.Generic;
using Wsdot.Wzdx.v4.Feeds;

namespace Wsdot.Wzdx.v4.Builders
{
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

        public FeedInfoBuilder WithPublisher(string value)
        {
            return new FeedInfoBuilder(_configuration, info => info.Publisher = value);
        }
        public FeedInfoBuilder WithVersion(Version value)
        {
            return new FeedInfoBuilder(_configuration, info => info.Version = value.ToString(2));
        }

        public FeedInfoBuilder WithUpdateFrequency(TimeSpan value)
        {
            return new FeedInfoBuilder(_configuration, info => info.UpdateFrequency = (int)value.TotalSeconds);
        }

        public FeedInfoBuilder WithUpdateDate(DateTimeOffset value)
        {
            return new FeedInfoBuilder(_configuration, info => info.UpdateDate = value);
        }

        public FeedInfoBuilder WithContactName(string value)
        {
            return new FeedInfoBuilder(_configuration, info => info.ContactName = value);
        }
        
        public FeedInfoBuilder WithContactEmail(string value)
        {
            return new FeedInfoBuilder(_configuration, info => info.ContactEmail = value);
        }
        
        public FeedInfoBuilder WithLicense(LicenseType value)
        {
            return new FeedInfoBuilder(_configuration, info => info.License = value);
        }

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