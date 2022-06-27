using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.RoadEvents
{
    /// <summary>
    /// Provides an immutable builder of a v4 Restriction class
    /// </summary>
    public class RestrictionBuilder : Builder<Restriction>
    {
        public RestrictionBuilder(RestrictionType type, UnitOfMeasurement unit) :
            base(new List<Action<Restriction>>(), restriction =>
            {
                restriction.Value = 0;
                restriction.Type = type;
                restriction.Unit = unit;
            })
        {
            // ignore
        }

        private RestrictionBuilder(IEnumerable<Action<Restriction>> configuration, Action<Restriction> step) : 
            base(configuration, step)
        {
            
        }

        [Pure]
        public RestrictionBuilder WithType(RestrictionType value)
        {
            return new RestrictionBuilder(Configuration, restriction => restriction.Type = value);
        }

        [Pure]
        public RestrictionBuilder WithUnitOfMeasure(UnitOfMeasurement value)
        {
            return new RestrictionBuilder(Configuration, restriction => restriction.Unit = value);
        }

        [Pure]
        public RestrictionBuilder WithValue(double value)
        {
            return new RestrictionBuilder(Configuration, restriction => restriction.Value = value);
        }

        protected override Func<Restriction> ResultFactory { get; } = () => new Restriction();
    }
}