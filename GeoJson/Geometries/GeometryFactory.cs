using System.Collections.Generic;

namespace Wsdot.Wzdx.GeoJson.Geometries
{
    public static class GeometryFactory
    {
        public static IGeometry CreatePoint(double longitude, double latitude)
        {
            return new Point()
            {
                Coordinates = new Position(longitude, latitude),
                BoundaryBox = new List<double>() { longitude, latitude, longitude, latitude }
            };
        }

        public static IGeometry CreateNull()
        {
            return new NullGeometry();
        }
    }
}
