using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Wzdx.GeoJson.Geometries
{
    internal class WktTokenizer
    {
        private readonly TextReader _reader;

        public WktTokenizer(TextReader reader)
        {
            _reader = reader;
        }

        public bool TryNextToken(out WktToken token)
        {
            while (TryPeekNextChar(_reader, out var value))
            {
                if (char.IsLetter(value))
                {
                    var text = ReadText(_reader);
                    token = new WktToken(WktToken.TokenType.Text, text);
                    return true;
                }

                if (IsCoordinateGroupStart(value))
                {
                    _reader.Read(); // skip ahead
                    token = new WktToken(WktToken.TokenType.StartCoordinateGroup, string.Empty);
                    return true;
                }

                if (IsCoordinateSeparator(value))
                {
                    _reader.Read(); // skip ahead
                    token = new WktToken(WktToken.TokenType.EndCoordinatePoint, string.Empty);
                    return true;
                }

                if (IsCoordinateGroupEnd(value))
                {
                    _reader.Read(); // skip ahead
                    token = new WktToken(WktToken.TokenType.EndCoordinateGroup, string.Empty);
                    return true;
                }

                if (IsNumeric(value))
                {
                    var number = ReadNumber(_reader);
                    token = new WktToken(WktToken.TokenType.Number, number);
                    return true;
                }

                _reader.Read();
            }

            token = null;
            return false;
        }

        private static bool IsCoordinateSeparator(char value)
        {
            return value == ',';
        }

        private static string ReadNumber(TextReader reader)
        {
            var result = new StringBuilder();
            var hasDecimal = false;

            while (TryPeekNextChar(reader, out var val) && IsNumeric(val))
            {
                // number must start with a sign, if any sign is present
                if (IsNumericSign(val) && result.Length > 0)
                    throw new FormatException("Number string is not in the correct format, unexpected sign");

                // number must only have one decimal point, if any point is present
                if (IsNumericDecimal(val) && hasDecimal)
                    throw new FormatException("Number string is not in the correct format, unexpected decimal point");

                hasDecimal = hasDecimal || IsNumericDecimal(val);
                result.Append((char)reader.Read());
            }

            return result.ToString();
        }

        private static bool IsNumericSign(char value)
        {
            return value == '-';
        }

        private static bool IsNumeric(char value)
        {
            return char.IsDigit(value) || value == '-' || value == '.';
        }

        private static bool IsNumericDecimal(char value)
        {
            return value == '.';
        }

        private static string ReadText(TextReader reader)
        {
            var result = new StringBuilder();
            while (TryPeekNextChar(reader, out var val)
                   && IsLetterOrWhitespace(val))
            {
                result.Append((char)reader.Read());
            }

            return result.ToString();
        }

        private static bool IsLetterOrWhitespace(char value)
        {
            return char.IsLetter(value) || char.IsWhiteSpace(value);
        }

        private static bool TryPeekNextChar(TextReader reader, out char val)
        {
            val = (char)reader.Peek();
            return val != char.MaxValue;
        }

        private static bool IsCoordinateGroupStart(char val)
        {
            return val == '(';
        }

        private static bool IsCoordinateGroupEnd(char val)
        {
            return val == ')';
        }
    }

    internal class WktToken
    {
        public TokenType Type { get; }
        public string Value { get; }

        public enum TokenType
        {
            None,
            Text,
            StartCoordinateGroup,
            EndCoordinateGroup,
            Number,
            EndCoordinatePoint
        }

        public WktToken(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }
    }
    
    public class Geometry
    {
        public static IGeometry FromWkt(string value)
        {
            // https://www.ibm.com/docs/en/db2-warehouse?topic=formats-well-known-text-wkt-format
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
                default:
                    throw new NotSupportedException($"Wkt Geometry '{type}' is not supported");
            }
        }
    }
}