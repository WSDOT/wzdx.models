using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wsdot.Wzdx.v3.WorkZones
{
    /// <summary>A description of the type of work being done in a road event and an indication of if that work will result in an architectural change to the roadway</summary>

    public class TypeOfWork 
    {
        [JsonProperty("type_name", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public WorkTypeName TypeName { get; set; }

        /// <summary>A flag indicating whether the type of work will result in an architectural change to the roadway</summary>
        [JsonProperty("is_architectural_change", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool IsArchitecturalChange { get; set; }

        private IDictionary<string, object> _additionalProperties = new Dictionary<string, object>();

        [JsonExtensionData]
        public IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties; }
            set { _additionalProperties = value; }
        }


    }
}