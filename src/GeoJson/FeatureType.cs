using System.Runtime.Serialization;

namespace Wzdx.GeoJson
{
    public enum FeatureType
    {
        [EnumMember(Value = nameof(FeatureCollection))]
        FeatureCollection,
        [EnumMember(Value = nameof(Feature))]
        Feature
    }
}