using System.Runtime.Serialization;

namespace Wsdot.Wzdx.GeoJson.Geometries
{
    public enum GeometryType
    {
        None = -1,
        [EnumMember(Value = @"Point")]
        Point = 0,
        [EnumMember(Value = @"MultiPoint")]
        MultiPoint = 1,
        [EnumMember(Value = @"LineString")]
        LineString = 2,
        [EnumMember(Value = @"MultiLineString")]
        MultiLineString = 3,
        [EnumMember(Value = @"Polygon")]
        Polygon = 4,
        [EnumMember(Value = @"MultiPolygon")]
        MultiPolygon = 5,
        [EnumMember(Value = @"GeometryCollection")]
        GeometryCollection = 6,
    }
}