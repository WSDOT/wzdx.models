
using System.Diagnostics.Contracts;
using Wzdx.Core;
using Wzdx.v4.WorkZones;

namespace Wzdx.v4.RoadEvents
{
    /// <summary>
    /// Provides a builder for a v4 Restriction class
    /// </summary>
    public class RestrictionBuilder : IBuilder<Restriction>
    {
        private BuilderConfiguration<Restriction> Configuration { get; }
            = new BuilderConfiguration<Restriction>();

        public RestrictionBuilder(RestrictionType type)
        {
            WithType(type);
            WithoutMeasure();
        }

        public RestrictionBuilder(RestrictionType type, UnitOfMeasurement unit, double value) : this(type)
        {
            WithMeasure(unit, value);
        }

        public RestrictionBuilder(RestrictionType type, UnitOfMeasurement unit) : this(type, unit, 0)
        {

        }

        public RestrictionBuilder WithType(RestrictionType value)
        {
            Configuration.Set(restriction => restriction.Type, value);
            return this;
        }

        public RestrictionBuilder WithMeasure(UnitOfMeasurement unit, double value)
        {
            Configuration.Set(restriction => restriction.Unit, unit);
            Configuration.Set(restriction => restriction.Value, value);
            return this;
        }

        public RestrictionBuilder WithoutMeasure()
        {
            Configuration.Default(restriction => restriction.Unit, null);
            Configuration.Default(restriction => restriction.Value, null);
            return this;
        }

        public RestrictionBuilder WithValue(double value)
        {
            Configuration.Set(restriction => restriction.Value, value);
            return this;
        }

        public RestrictionBuilder WithoutValue()
        {
            Configuration.Default(restriction => restriction.Value, null);
            return this;
        }

        [Pure]
        public Restriction Result()
        {
            var result = new Restriction();
            Configuration.ApplyTo(result);
            return result;
        }
    }
}