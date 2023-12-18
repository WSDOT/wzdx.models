using System;
using System.Collections.Generic;
using System.IO;
using Wzdx.GeoJson.Geometries.Wkt;

namespace Wzdx.GeoJson.Geometries
{
    public class Geometry
    {
        public static IGeometry FromWkt(string value)
        {
            // https://www.ibm.com/docs/en/db2-warehouse?topic=formats-well-known-text-wkt-format
            // note: geometry with m - measure - is not supported in geo-json and should be ignored or throw an exception

            using (var reader = new StringReader(value))
            {
                return FromWkt(reader);
            }
        }

        public static IGeometry FromWkt(TextReader reader)
        {
            var tokenizer = new WktTokenizer(reader);
            var type = string.Empty;
            var coordinates = new List<double>();

            var positions = new List<IPosition>();
            while (tokenizer.TryNextToken(out var token))
            {
                switch (token.Type)
                {
                    case WktToken.TokenType.Text:
                        type = type.Length == 0
                            ? token.Value.ToUpper()
                            : type + " " + token.Value.ToUpper();
                        break;
                    case WktToken.TokenType.Number:
                        coordinates.Add(double.Parse(token.Value));
                        break;
                    case WktToken.TokenType.StartCoordinateGroup:
                        break;
                    case WktToken.TokenType.EndCoordinatePoint:
                    case WktToken.TokenType.EndCoordinateGroup:
                        if (coordinates.Count > 0)
                            positions.Add(Position.From(coordinates));

                        coordinates.Clear();
                        break;
                    case WktToken.TokenType.None:
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            switch (type.Trim())
            {
                case "POINT":
                case "POINT Z":
                    return GeometryFactory.CreatePoint(positions);
                case "LINESTRING":
                case "LINESTRING Z":
                    return GeometryFactory.CreateLineString(positions);
                case "MULTIPOINT":
                case "MULTIPOINT Z":
                    return GeometryFactory.CreateMultiPoint(positions);
                    
                // review: handling of multilinestring
                // review: handling of multipolygon
                // review: handling of geometrycollection

                default:
                    throw new NotSupportedException($"Wkt Geometry '{type}' is not supported");
            }
        }
    }
}