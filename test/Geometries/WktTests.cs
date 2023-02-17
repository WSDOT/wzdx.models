using System;
using System.Collections.Generic;
using System.Linq;
using Wzdx.GeoJson.Geometries;
using Xunit;

namespace Wzdx.Models.Tests.Geometries
{
    internal sealed class PointGeometryComparer : IEqualityComparer<Point>,
        IEqualityComparer<IGeometry>
    {
        public bool Equals(Point x, Point y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Type == y.Type
                   && Equals(x.Coordinates, y.Coordinates)
                   && x.BoundaryBox.SequenceEqual(y.BoundaryBox);
        }

        public int GetHashCode(Point obj)
        {
            return HashCode.Combine((int)obj.Type, obj.Coordinates, obj.BoundaryBox);
        }

        public bool Equals(IGeometry x, IGeometry y)
        {
            return Equals(x as Point, y as Point);
        }

        public int GetHashCode(IGeometry obj)
        {
            return obj is Point geometry
                ? GetHashCode(geometry)
                : 0;
        }
    }

    internal sealed class LineStringGeometryComparer : IEqualityComparer<LineString>,
        IEqualityComparer<IGeometry>
    {
        public bool Equals(LineString x, LineString y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Type == y.Type
                   && x.Coordinates.SequenceEqual(y.Coordinates)
                   && x.BoundaryBox.SequenceEqual(y.BoundaryBox);
        }

        public int GetHashCode(LineString obj)
        {
            return HashCode.Combine((int)obj.Type, obj.Coordinates, obj.BoundaryBox);
        }

        public bool Equals(IGeometry x, IGeometry y)
        {
            return Equals(x as LineString, y as LineString);
        }

        public int GetHashCode(IGeometry obj)
        {
            return obj is LineString geometry
                ? GetHashCode(geometry)
                : 0;
        }
    }
    
    internal sealed class MultiPointGeometryComparer : IEqualityComparer<MultiPoint>,
        IEqualityComparer<IGeometry>
    {
        public bool Equals(MultiPoint x, MultiPoint y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Type == y.Type
                   && x.Coordinates.SequenceEqual(y.Coordinates)
                   && x.BoundaryBox.SequenceEqual(y.BoundaryBox);
        }

        public int GetHashCode(MultiPoint obj)
        {
            return HashCode.Combine((int)obj.Type, obj.Coordinates, obj.BoundaryBox);
        }

        public bool Equals(IGeometry x, IGeometry y)
        {
            return Equals(x as MultiPoint, y as MultiPoint);
        }

        public int GetHashCode(IGeometry obj)
        {
            return obj is MultiPoint geometry
                ? GetHashCode(geometry)
                : 0;
        }
    }
    
    public class WktTests
    {
        [Fact]
        public void EmptyShouldThrowException()
        {
            const string wkt = "";
            Assert.Throws<NotSupportedException>(() => Geometry.FromWkt(wkt));
        }

        [Fact]
        public void WkPointTextWithWhiteSpaceShouldReturnPointGeometry()
        {
            const string wkt = " POINT ( -15.02  12.28 ) ";
            var expected = GeometryFactory.CreatePoint(-15.02, 12.28);
            var actual = Geometry.FromWkt(wkt);
            Assert.Equal(expected, actual, new PointGeometryComparer());
        }


        [Fact]
        public void WkPointTextShouldReturnPointGeometry()
        {
            const string wkt = "POINT(10 -10.28)";
            var expected = GeometryFactory.CreatePoint(10, -10.28);
            var actual = Geometry.FromWkt(wkt);
            Assert.Equal(expected, actual, new PointGeometryComparer());
        }

        [Fact]
        public void WkPointWithAltitudeTextShouldReturnPointGeometry()
        {
            const string wkt = "POINT(7 -10.28 2)";
            var expected = GeometryFactory.CreatePoint(new[] { 7, -10.28, 2 });
            var actual = Geometry.FromWkt(wkt);
            Assert.Equal(expected, actual, new PointGeometryComparer());
        }

        [Fact]
        public void WkPointZTextShouldReturnPointGeometry()
        {
            const string wkt = "POINT Z(-5.5 13.28 5)";
            var expected = GeometryFactory.CreatePoint(new[] { -5.5, 13.28, 5 });
            var actual = Geometry.FromWkt(wkt);
            Assert.Equal(expected, actual, new PointGeometryComparer());
        }

        [Fact]
        public void WkPointMTextShouldThrowException()
        {
            const string wkt = "POINT M(12.05 12 -7.2)";
            Assert.Throws<NotSupportedException>(() => Geometry.FromWkt(wkt));
        }

        [Fact]
        public void WkPointZMTextShouldThrowException()
        {
            const string wkt = "POINT ZM(12.05 12 -7.2)";
            Assert.Throws<NotSupportedException>(() => Geometry.FromWkt(wkt));
        }

        [Fact]
        public void WkLineStringTextShouldReturnGeometry()
        {
            const string wkt = "LINESTRING(10.05 10, 10.05 10.1, 11.05 10)";
            var expected = GeometryFactory.CreateLineString(new[]
            {
                new Position(10.05, 10),
                new Position(10.05, 10.1),
                new Position(11.05, 10)
            });
            var actual = Geometry.FromWkt(wkt);
            Assert.Equal(expected, actual, new LineStringGeometryComparer());
        }

        [Fact]
        public void WkLineStringWithAltitudeTextShouldReturnGeometry()
        {
            const string wkt = "LINESTRING(7 -10 10.05, -12.85 15.5 -5)";
            var expected = GeometryFactory.CreateLineString(new[]
            {
                new Position(7, -10, 10.05),
                new Position(-12.85, 15.5, -5)
            });
            var actual = Geometry.FromWkt(wkt);
            Assert.Equal(expected, actual, new LineStringGeometryComparer());
        }

        [Fact]
        public void WkMultiPointTextShouldReturnGeometry()
        {
            const string wkt = "MULTIPOINT(7 -10 10.05, -12.85 15.5 -5)";
            var expected = GeometryFactory.CreateMultiPoint(new[]
            {
                new Position(7, -10, 10.05),
                new Position(-12.85, 15.5, -5)
            });
            var actual = Geometry.FromWkt(wkt);
            Assert.Equal(expected, actual, new MultiPointGeometryComparer());
        }

        [Fact]
        public void WkMultiPointTextAlternateShouldReturnGeometry()
        {
            const string wkt = "MULTIPOINT((-10 10.05), (-12.85 15.2))";
            var expected = GeometryFactory.CreateMultiPoint(new[]
            {
                new Position(-10, 10.05),
                new Position(-12.85, 15.2)
            });
            var actual = Geometry.FromWkt(wkt);
            Assert.Equal(expected, actual, new MultiPointGeometryComparer());
        }
    }
}
