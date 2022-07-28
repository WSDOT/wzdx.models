using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Wzdx.GeoJson;
using Wzdx.v4.WorkZones;

namespace Wzdx.v4.Feeds
{
    /// <summary>
    /// The GeoJSON output of a WZDx feed data feed (v4.0)
    /// </summary>
    [DataContract(Name = "WZDxFeed")]
    public class RoadEventsFeed : FeatureCollection<RoadEventFeature>
    {
        [JsonProperty("road_event_feed_info", Required = Required.Always)]
        [Required]
        public FeedInfo FeedInfo { get; set; } = new FeedInfo();

        public static RoadEventsFeed Create(string publisher, IEnumerable<FeedDataSource> dataSources, Version version)
        {
            var feed = new RoadEventsFeed();
            
            feed.FeedInfo.Publisher = publisher;
            feed.FeedInfo.Version = version.ToString();
            feed.FeedInfo.DataSources = dataSources.ToList();
            feed.FeedInfo.License = LicenseType.HttpsCreativeCommonsOrgPublicDomainZero10;
            feed.FeedInfo.UpdateFrequency = int.MaxValue;
            feed.FeedInfo.UpdateDate = DateTimeOffset.UtcNow;

            return feed;
        }
    }
}