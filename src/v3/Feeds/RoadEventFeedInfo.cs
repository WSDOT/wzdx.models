using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wsdot.Wzdx.v3.Feeds
{
    /// <summary>Describes WZDx feed header information such as metadata, contact information, and data sources</summary>

    public class RoadEventFeedInfo 
    {
        /// <summary>The organization responsible for publishing the feed</summary>
        [JsonProperty("publisher", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string Publisher { get; set; }

        /// <summary>The name of the individual or group responsible for the data feed</summary>
        [JsonProperty("contact_name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Contact_name { get; set; }

        /// <summary>The email address of the individual or group responsible for the data feed</summary>
        [JsonProperty("contact_email", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Contact_email { get; set; }

        /// <summary>The frequency in seconds at which the data feed is updated</summary>
        [JsonProperty("update_frequency", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [Range(1, int.MaxValue)]
        public int? Update_frequency { get; set; }

        /// <summary>The UTC date and time when the data feed was last updated</summary>
        [JsonProperty("update_date", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public System.DateTimeOffset Update_date { get; set; }

        /// <summary>The WZDx specification version used to create the data feed, in 'major.minor' format</summary>
        [JsonProperty("version", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        [RegularExpression(@"^(0|[1-9][0-9]*)\.(0|[1-9][0-9]*)$")]
        public string Version { get; set; }

        /// <summary>The URL of the license that applies to the data in the WZDx feed. This *must* be the string "https://creativecommons.org/publicdomain/zero/1.0/"</summary>
        [JsonProperty("license", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public RoadEventFeedInfoLicense? License { get; set; }

        /// <summary>A list of specific data sources for the road event data in the feed</summary>
        [JsonProperty("data_sources", Required = Required.Always)]
        [Required]
        [MinLength(1)]
        public ICollection<RoadEventDataSource> Data_sources { get; set; } = new Collection<RoadEventDataSource>();

        private IDictionary<string, object> _additionalProperties = new Dictionary<string, object>();

        [JsonExtensionData]
        public IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties; }
            set { _additionalProperties = value; }
        }
    }
}