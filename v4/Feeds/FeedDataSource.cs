using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Wsdot.Wzdx.v4.Feeds
{
    /// <summary>
    /// Describes information about a specific data source used to build the work zone data feed
    /// </summary>
    
    [DataContract(Name = "FeedDataSource")]
    public class FeedDataSource
    {
        /// <summary>
        /// Unique identifier for the organization providing work zone data
        /// </summary>
        [JsonProperty("data_source_id", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        [DataMember(Name = "data_source_id")]
        public string DataSourceId { get; set; }

        /// <summary>
        /// The name of the organization for the authoritative source of the work zone data
        /// </summary>
        [JsonProperty("organization_name", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        [DataMember(Name = "organization_name")]
        public string OrganizationName { get; set; }

        /// <summary>
        /// The name of the individual or group responsible for the data source
        /// </summary>
        [JsonProperty("contact_name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [DataMember(Name = "contact_name")]
        public string ContactName { get; set; }

        /// <summary>
        /// The email address of the individual or group responsible for the data source
        /// </summary>
        [JsonProperty("contact_email", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ContactEmail { get; set; }

        /// <summary>
        /// The frequency in seconds at which the data source is updated
        /// </summary>
        [JsonProperty("update_frequency", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [Range(1, int.MaxValue)]
        public int UpdateFrequency { get; set; }

        /// <summary>
        /// The UTC date and time when the data source was last updated
        /// </summary>
        [JsonProperty("update_date", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset UpdateDate { get; set; }

        /// <summary>
        /// **DEPRECATED** Describes the type of linear referencing system used for the milepost measurements
        /// </summary>
        [JsonProperty("lrs_type", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string LrsType { get; set; }

        /// <summary>
        /// **DEPRECATED** A URL where additional information on the LRS information and transformation information is stored
        /// </summary>
        [JsonProperty("lrs_url", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Uri LrsUrl { get; set; }

        /// <summary>
        /// ***DEPRECATED***The method used to verify the accuracy of the location information
        /// </summary>
        [JsonProperty("location_verify_method", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string LocationVerifyMethod { get; set; }

        public static FeedDataSource Create(string id, string organizationName)
        {
            return new FeedDataSource()
            {
                DataSourceId = id,
                OrganizationName = organizationName,
                UpdateFrequency = int.MaxValue,
                UpdateDate = DateTimeOffset.UtcNow
            };
        }
    }
}