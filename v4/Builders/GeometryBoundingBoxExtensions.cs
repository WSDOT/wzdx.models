using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.GeoJson.Geometries;

namespace Wsdot.Wzdx.v4.Builders
{
    internal static class GeometryBoundingBoxExtensions
    {
        public static IEnumerable<double> GetBoundaryBox(this IGeometry geometry)
        {
            if (geometry == null) return Enumerable.Empty<double>();
            switch (geometry.Type)
            {
                case GeometryType.None:
                    return new double[4];
                case GeometryType.Point:
                    return new[] { ((Point)geometry).Coordinates, ((Point)geometry).Coordinates }.AsBoundaryBox();
                case GeometryType.MultiPoint:
                    return ((MultiPoint)geometry).Coordinates.AsBoundaryBox();
                case GeometryType.LineString:
                    return ((LineString)geometry).Coordinates.AsBoundaryBox();
                default:
                    throw new ArgumentOutOfRangeException(nameof(geometry.Type));
            }
        }
    }
}