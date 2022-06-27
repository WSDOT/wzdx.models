using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.RoadEvents
{
    /// <summary>
    /// Provides an immutable builder of a v4 Lane class
    /// </summary>
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

        [Pure]
        public LaneBuilder WithType(LaneType value)
        {
            return new LaneBuilder(Configuration, lane => lane.Type = value);
        }

        [Pure]
        public LaneBuilder WithStatus(LaneStatus value)
        {
            return new LaneBuilder(Configuration, lane => lane.Status = value);
        }

        [Pure]
        public LaneBuilder WithOrder(int value)
        {
            return value <= 0
                ? throw new ArgumentOutOfRangeException(nameof(value), value, "Order must be greater than zero.")
                : new LaneBuilder(Configuration, lane => lane.Order = value);
        }

        [Pure]
        public LaneBuilder WithRestriction(RestrictionType type, UnitOfMeasurement unit, Func<RestrictionBuilder, RestrictionBuilder> configure)
        {
            var restriction = configure(new RestrictionBuilder(type, unit)).Result();
            return new LaneBuilder(Configuration, lane =>
            {
                if (lane.Restrictions == null) lane.Restrictions = new List<Restriction>();
                lane.Restrictions.Add(restriction);
            });
        }

        [Pure]
        public LaneBuilder WithNoRestrictions()
        {
            return new LaneBuilder(Configuration, lane => lane.Restrictions = null);
        }

        protected override Func<Lane> ResultFactory { get; } = () => new Lane();
    }
}