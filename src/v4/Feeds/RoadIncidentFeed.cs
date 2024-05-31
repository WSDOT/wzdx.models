using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using Wzdx.GeoJson;
using Wzdx.v4.WorkZones;

namespace Wzdx.v4.Feeds
{
    /// <summary>
    /// The GeoJSON output of a WZDx road Incident data feed (v4.0)
    /// </summary>
    public class RoadIncidentFeed : FeatureCollection<RoadEventFeature>
    {
        [JsonProperty("feed_info", Required = Required.Always)]
        [Required]
        public FeedInfo FeedInfo { get; set; } = new FeedInfo();

        public static RoadIncidentFeed Create(string publisher, IEnumerable<FeedDataSource> dataSources, Version version)
        {
            var feed = new RoadIncidentFeed();

            feed.FeedInfo.Publisher = publisher;
            feed.FeedInfo.Version = version.ToString();
            feed.FeedInfo.DataSources = dataSources.ToList();
            feed.FeedInfo.UpdateFrequency = int.MaxValue;
            feed.FeedInfo.UpdateDate = DateTimeOffset.UtcNow;

            return feed;
        }
    }
}