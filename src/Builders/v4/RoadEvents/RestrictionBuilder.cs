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

        public RestrictionBuilder(RestrictionType type, UnitOfMeasurement unit) 
        {
            WithType(type);
            WithUnitOfMeasure(unit);
            WithValue(0);
        }
        
        public RestrictionBuilder WithType(RestrictionType value)
        {
            Configuration.Set(restriction => restriction.Type, value);
            return this;
        }
        
        public RestrictionBuilder WithUnitOfMeasure(UnitOfMeasurement value)
        {
            Configuration.Set(restriction => restriction.Unit, value);
            return this;
        }

        public RestrictionBuilder WithValue(double value)
        {
            Configuration.Set(restriction => restriction.Value, value);
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