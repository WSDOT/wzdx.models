using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Wsdot.Wzdx.v4.Devices
{
    /// <summary>
    /// Any GPS-enabled ITS device that is placed at a point on a roadway to dynamically know the location of something (often the beginning or end of a work zone)
    /// </summary>
    public class LocationMarker : IFieldDevice
    {
        [JsonProperty("core_details", Required = Required.Always)]
        [Required]
        public FieldDeviceCoreDetails CoreDetails { get; set; } = new FieldDeviceCoreDetails();

        [JsonProperty("marked_locations", Required = Required.Always)]
        [Required]
        [MinLength(1)]
        public ICollection<MarkedLocation> MarkedLocations { get; set; } = new Collection<MarkedLocation>();
    }
}