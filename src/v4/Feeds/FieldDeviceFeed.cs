using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Wzdx.GeoJson;
using Wzdx.v4.Devices;

namespace Wzdx.v4.Feeds
{
    /// <summary>
    /// The SwzDeviceFeed contains information (location, status, live data) about field devices deployed on the roadway in work zones.
    /// </summary>
    [DataContract(Name = "SwzDeviceFeed")]
    public class FieldDeviceFeed : FeatureCollection<FieldDeviceFeature>
    {
        [JsonProperty("feed_info", Required = Required.Always)]
        [Required]
        public FeedInfo FeedInfo { get; set; } = new FeedInfo();
        
        public static FieldDeviceFeed Create(string publisher, IEnumerable<FeedDataSource> dataSources, Version version)
        {
            var feed = new FieldDeviceFeed();

            feed.FeedInfo.Publisher = publisher;
            feed.FeedInfo.Version = version.ToString();
            feed.FeedInfo.DataSources = dataSources.ToList();
            feed.FeedInfo.UpdateFrequency = int.MaxValue;
            feed.FeedInfo.UpdateDate = DateTimeOffset.UtcNow;

            return feed;
        }
    }
}