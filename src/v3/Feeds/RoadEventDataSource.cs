using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Wzdx.v3.WorkZones;

namespace Wzdx.v3.Feeds
{
    /// <summary>Describes information about a specific data source used to build the work zone data feed</summary>

    public class RoadEventDataSource 
    {
        /// <summary>Unique identifier for the organization providing work zone data</summary>
        [JsonProperty("data_source_id", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string Data_source_id { get; set; }

        /// <summary>The name of the organization for the authoritative source of the work zone data</summary>
        [JsonProperty("organization_name", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string Organization_name { get; set; }

        /// <summary>The name of the individual or group responsible for the data source</summary>
        [JsonProperty("contact_name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Contact_name { get; set; }

        /// <summary>The email address of the individual or group responsible for the data source</summary>
        [JsonProperty("contact_email", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Contact_email { get; set; }

        /// <summary>The frequency in seconds at which the data source is updated</summary>
        [JsonProperty("update_frequency", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [Range(1, int.MaxValue)]
        public int? Update_frequency { get; set; }

        /// <summary>The UTC date and time when the data source was last updated</summary>
        [JsonProperty("update_date", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? Update_date { get; set; }

        [JsonProperty("location_method", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public LocationMethod Location_method { get; set; }

        /// <summary>The method used to verify the accuracy of the location information</summary>
        [JsonProperty("location_verify_method", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Location_verify_method { get; set; }

        /// <summary>Describes the type of linear referencing system used for the milepost measurements</summary>
        [JsonProperty("lrs_type", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Lrs_type { get; set; }

        /// <summary>A URL where additional information on the LRS information and transformation information is stored</summary>
        [JsonProperty("lrs_url", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Uri Lrs_url { get; set; }

        private IDictionary<string, object> _additionalProperties = new Dictionary<string, object>();

        [JsonExtensionData]
        public IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties; }
            set { _additionalProperties = value; }
        }


    }
}