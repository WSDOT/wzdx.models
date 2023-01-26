using System;
using System.IO;
using System.Text;

namespace Wzdx.GeoJson.Geometries.Wkt
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
}