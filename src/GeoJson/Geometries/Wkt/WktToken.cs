namespace Wzdx.GeoJson.Geometries.Wkt
{
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
}