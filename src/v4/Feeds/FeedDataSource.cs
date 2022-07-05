using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Wsdot.Wzdx.v4.Feeds
{
    /// <summary>
    /// Describes information about a specific data source used to build the work zone data feed
    /// </summary>
    
    [DataContract(Name = "FeedDataSource")]
    public class FeedDataSource : IEquatable<FeedDataSource>
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
        public int? UpdateFrequency { get; set; }

        /// <summary>
        /// The UTC date and time when the data source was last updated
        /// </summary>
        [JsonProperty("update_date", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? UpdateDate { get; set; }

        /// <summary>
        /// **DEPRECATED** Describes the type of linear referencing system used for the milepost measurements
        /// </summary>
        [JsonProperty("lrs_type", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string LrsType { get; set; }

        /// <summary>
        /// **DEPRECATED** A URL where additional information on the LRS information and transformation information is stored
        /// </summary>
        [JsonProperty("lrs_url", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        // todo: format validator
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
                OrganizationName = organizationName
            };
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as FeedDataSource);
        }

        public bool Equals(FeedDataSource other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return DataSourceId == other.DataSourceId &&
                   OrganizationName == other.OrganizationName &&
                   ContactName == other.ContactName &&
                   ContactEmail == other.ContactEmail &&
                   UpdateFrequency == other.UpdateFrequency &&
                   UpdateDate.Equals(other.UpdateDate) &&
                   LrsType == other.LrsType &&
                   Equals(LrsUrl,
                       other.LrsUrl) &&
                   LocationVerifyMethod == other.LocationVerifyMethod;
        }

        [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "Supressing Resharper Code Quality Check")]
        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (DataSourceId != null ? DataSourceId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (OrganizationName != null ? OrganizationName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ContactName != null ? ContactName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ContactEmail != null ? ContactEmail.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UpdateFrequency != null ? UpdateFrequency.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ UpdateDate.GetHashCode();
                hashCode = (hashCode * 397) ^ (LrsType != null ? LrsType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (LrsUrl != null ? LrsUrl.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (LocationVerifyMethod != null ? LocationVerifyMethod.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(FeedDataSource left, FeedDataSource right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FeedDataSource left, FeedDataSource right)
        {
            return !Equals(left, right);
        }
    }
}