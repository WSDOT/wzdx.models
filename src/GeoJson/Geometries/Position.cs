using System;
using System.Collections.Generic;
using System.Linq;

namespace Wzdx.GeoJson.Geometries
{
    /// <summary>
    /// A position represent the longitude, latitude, and optionally altitude of a coordinate
    /// </summary>
    public interface IPosition
    {
        /// <summary>
        /// Decimal degrees of latitude
        /// </summary>
        double Latitude { get; }
        
        /// <summary>
        /// Decimal degrees of longitude
        /// </summary>
        double Longitude { get; }

        /// <summary>
        /// Height meters above or below spatial reference ellipsoid.
        /// </summary>
        double? Altitude { get; }
    }

    public sealed class Position : IPosition, IEquatable<Position>
    {
        private const string PositionValuesLengthToShort = "Position values length invalid (expected at least 2).";
        private const string PositionValuesLengthToLong = "Position values length invalid (expected at most 3).";


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

        public IEnumerable<double> Values()
        {
            yield return Longitude;
            yield return Latitude;
            if (Altitude.HasValue)
                yield return Altitude.Value;
        }

        public static Position From(params double[] values)
        {
            return From(values.AsEnumerable());
        }

        public static Position From(IEnumerable<double> values)
        {
            var enumerator = values.GetEnumerator();
            if (!enumerator.MoveNext()) throw new ArgumentException(PositionValuesLengthToShort, nameof(values));
            
            var longitude = enumerator.Current;

            if (!enumerator.MoveNext()) throw new ArgumentException(PositionValuesLengthToShort, nameof(values));

            var latitude = enumerator.Current;
            var altitude = enumerator.MoveNext()
                ? enumerator.Current
                : (double?)null;

            if (enumerator.MoveNext()) throw new ArgumentException(PositionValuesLengthToLong, nameof(values));

            enumerator.Dispose();

            return new Position(longitude, latitude, altitude);
        }

        public static explicit operator Position(double[] values)
        {
            return From(values);
        }

        public static explicit operator double[](Position value)
        {
            return value.Values().ToArray();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Position);    
        }

        public bool Equals(Position other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Latitude.Equals(other.Latitude) && Longitude.Equals(other.Longitude) && Nullable.Equals(Altitude, other.Altitude);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Latitude.GetHashCode();
                hashCode = (hashCode * 397) ^ Longitude.GetHashCode();
                hashCode = (hashCode * 397) ^ Altitude.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Position left, Position right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !Equals(left, right);
        }
    }

    public static class PositionConversionExtensions {
        
        /// <summary>
        /// Calculate boundary box from sequence of coordinate positions
        /// </summary>
        /// <param name="coordinates">Sequence of positions</param>
        /// <returns></returns>
        public static IEnumerable<double> AsBoundaryBox(this IEnumerable<IPosition> coordinates)
        {
            var southwestern = new Position(double.MaxValue, double.MaxValue);
            var northeastern = new Position(double.MinValue, double.MinValue);

            // calculate the south-eastern and north-western positions
            foreach (var coordinate in coordinates)
            {
                // check coordinate against southwestern position
                if (coordinate.Longitude < southwestern.Longitude)
                    southwestern = new Position(coordinate.Longitude, southwestern.Latitude, southwestern.Altitude);

                if (coordinate.Latitude < southwestern.Latitude)
                    southwestern = new Position(southwestern.Longitude, coordinate.Latitude, southwestern.Altitude);

                if (coordinate.Altitude.GetValueOrDefault(0) < southwestern.Altitude.GetValueOrDefault(0))
                    southwestern = new Position(coordinate.Longitude, southwestern.Latitude, southwestern.Altitude);

                // check coordinate against northeastern position
                if (coordinate.Longitude > northeastern.Longitude)
                    northeastern = new Position(coordinate.Longitude, northeastern.Latitude, northeastern.Altitude);

                if (coordinate.Latitude > northeastern.Latitude)
                    northeastern = new Position(northeastern.Longitude, coordinate.Latitude, northeastern.Altitude);

                if (coordinate.Altitude.GetValueOrDefault(0) > northeastern.Altitude.GetValueOrDefault(0))
                    northeastern = new Position(coordinate.Longitude, coordinate.Latitude, northeastern.Altitude);
            }

            return southwestern.Altitude.HasValue || northeastern.Altitude.HasValue
                ? new[]
                {
                    southwestern.Longitude, southwestern.Latitude, southwestern.Altitude.GetValueOrDefault(0),
                    northeastern.Longitude, northeastern.Latitude, northeastern.Altitude.GetValueOrDefault(0)
                }
                : new[] {
                    southwestern.Longitude, southwestern.Latitude,
                    northeastern.Longitude, northeastern.Latitude
                };
        }
        public static IEnumerable<double> Values(this IPosition position)
        {
            yield return position.Longitude;
            yield return position.Latitude;
            if (position.Altitude.HasValue)
                yield return position.Altitude.Value;
        }
    }
}