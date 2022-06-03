using System;
using System.Collections.Generic;
using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.RoadEvents
{
    public class LaneBuilder : Builder<Lane>
    {
        public LaneBuilder(LaneType type, LaneStatus status, int order)
            : base(new List<Action<Lane>>(), lane =>
            {
                lane.Type = type;
                lane.Status = status;
                lane.Order = order;
            })
        {
            if (order <= 0)
                throw new ArgumentOutOfRangeException(nameof(order), order, "Order must be greater than zero.");
        }

        private LaneBuilder(IEnumerable<Action<Lane>> configuration, Action<Lane> step) : base(configuration, step)
        {
            
        }

        public LaneBuilder WithType(LaneType value)
        {
            return new LaneBuilder(Configuration, lane => lane.Type = value);
        }

        public LaneBuilder WithStatus(LaneStatus value)
        {
            return new LaneBuilder(Configuration, lane => lane.Status = value);
        }

        public LaneBuilder WithOrder(int value)
        {
            return value <= 0
                ? throw new ArgumentOutOfRangeException(nameof(value), value, "Order must be greater than zero.")
                : new LaneBuilder(Configuration, lane => lane.Order = value);
        }
        
        public LaneBuilder WithRestriction(RestrictionType type, UnitOfMeasurement unit, Func<RestrictionBuilder, RestrictionBuilder> configure)
        {
            var restriction = configure(new RestrictionBuilder(type, unit)).Result();
            return new LaneBuilder(Configuration, lane => lane.Restrictions.Add(restriction));
        }

        public LaneBuilder WithNoRestrictions()
        {
            return new LaneBuilder(Configuration, lane => lane.Restrictions.Clear());
        }

        protected override Func<Lane> ResultFactory { get; } = () => new Lane();
    }
}