using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using Wsdot.Wzdx.GeoJson;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.Feeds
{
    /// <summary>
    /// The GeoJSON output of a WZDx road restriction data feed (v4.0)
    /// </summary>
    public class RoadRestrictionFeed : FeatureCollection<RoadEventFeature>
    {
        [JsonProperty("feed_info", Required = Required.Always)]
        [Required]
        public FeedInfo FeedInfo { get; set; } = new FeedInfo();

        public static RoadRestrictionFeed Create(string publisher, IEnumerable<FeedDataSource> dataSources, Version version)
        {
            var feed = new RoadRestrictionFeed();

            feed.FeedInfo.Publisher = publisher;
            feed.FeedInfo.Version = version.ToString();
            feed.FeedInfo.DataSources = dataSources.ToList();
            feed.FeedInfo.UpdateFrequency = int.MaxValue;
            feed.FeedInfo.UpdateDate = DateTimeOffset.UtcNow;

            return feed;
        }
    }
}