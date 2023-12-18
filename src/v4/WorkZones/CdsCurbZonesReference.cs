using Newtonsoft.Json;

namespace Wzdx.v4.WorkZones
{
	/// <summary>
	/// The CdsCurbZonesReference object describes specific Curb Zones that are impacted by a work zone via an external reference to the Curb Data Specification's Curb API.
	/// Read more about the Open Mobility Foundation's Curb Data Specification (https://www.openmobilityfoundation.org/about-cds).
	/// </summary>
	public class CdsCurbZonesReference
	{
		/// <summary>
		/// A list of CDS Curb Zone ids.
		/// </summary>
		[JsonProperty("cds_curb_zone_ids", Required = Required.Always)]
		public int[] Ids { get; set; }

		/// <summary>
		/// An identifier for the source of the requested CDS Curbs API. This MUST be a full HTTPS URL pointing to the main curbs API that contains detailed information about each curb zone identified in cds_curb_zone_ids
		/// </summary>
		[JsonProperty("cds_curbs_api_url", Required = Required.Always)]
		public string Url { get; set; }
	}
}
