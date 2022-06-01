using System;

namespace Wsdot.Wzdx.GeoJson.Geometries
{
    public interface IPosition
    {
        double Latitude { get; }
        double Longitude { get; }
        double? Altitude { get; }
    }

    public sealed class Position : IPosition
    {
        public Position(double longitude, double latitude) : this(longitude, latitude, null)
        {
            // ignore
        }

        public Position(double longitude, double latitude, double? altitude)
        {
            Longitude = Math.Round(longitude, 6);
            Latitude = Math.Round(latitude, 6);
            Altitude = altitude.HasValue 
                ? Math.Round(altitude.Value, 6) 
                : (double?)null;
        }

        public double Latitude { get; }
        public double Longitude { get; }
        public double? Altitude { get; }
    }
}