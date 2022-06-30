using System.Runtime.Serialization;

namespace Wsdot.Wzdx.GeoJson
{
    public enum FeatureType
    {
        [EnumMember(Value = nameof(FeatureCollection))]
        FeatureCollection,
        [EnumMember(Value = nameof(Feature))]
        Feature
    }
}