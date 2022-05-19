namespace Wsdot.Wzdx.GeoJson.Geometries
{
    public interface IPosition
    {
        double Latitude { get; }
        double Longitude { get; }
    }
    public sealed class Position : IPosition
    {
        public Position(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        public double Latitude { get; }
        public double Longitude { get; }
    }
}