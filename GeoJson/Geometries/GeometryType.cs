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
        LineString = 2
        // for full GeoJson (https://datatracker.ietf.org/doc/html/rfc7946) support need to add: 
        // * MultiLineString
        // * Polygon
        // * MultiPolygon
        // * GeometryCollection
        //
    }
}