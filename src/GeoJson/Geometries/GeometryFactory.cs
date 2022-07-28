using System.Collections.Generic;

namespace Wzdx.GeoJson.Geometries
{
    public static class GeometryFactory
    {
        /// <summary>
        /// Creates an instance of Geometry representing a point
        /// </summary>
        /// <returns>Instance of Geometry</returns>
        public static IGeometry CreatePoint(double longitude, double latitude)
        {
            return CreatePoint(new[] { longitude, latitude });
        }

        /// <summary>
        /// Creates an instance of Geometry representing a point
        /// </summary>
        /// <returns>Instance of Geometry</returns>
        public static IGeometry CreatePoint(IEnumerable<double> values)
        {
            return Point.FromCoordinates(new [] { Position.From(values) });
        }

        /// <summary>
        /// Creates an instance of Geometry representing a line
        /// </summary>
        /// <returns>Instance of Geometry</returns>
        public static IGeometry CreateLineString(IEnumerable<IPosition> values)
        {
            return LineString.FromCoordinates(values);
        }

        /// <summary>
        /// Creates an instance of Geometry representing a series of points
        /// </summary>
        /// <returns>Instance of Geometry</returns>
        public static IGeometry CreateMultiPoint(IEnumerable<IPosition> values)
        {
            return MultiPoint.FromCoordinates(values);
        }

        /// <summary>
        /// Creates an instance of Geometry that does not contain any values
        /// </summary>
        /// <returns>Instance of Geometry</returns>
        public static IGeometry CreateNull()
        {
            return new NullGeometry();
        }

    }
}
