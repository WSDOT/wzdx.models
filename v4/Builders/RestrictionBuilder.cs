using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.Builders
{
    public class RestrictionBuilder
    {
        private readonly RestrictionType _type;
        private readonly UnitOfMeasurement _unit;
        private double _value;

        public RestrictionBuilder(RestrictionType type, UnitOfMeasurement unit)
        {
            _type = type;
            _unit = unit;
        }

        public RestrictionBuilder WithValue(double value)
        {
            _value = value;
            return this;
        }

        public Restriction Result()
        {
            return new Restriction()
            {
                Type = _type,
                Unit = _unit,
                Value = _value
            };
        }
    }
}