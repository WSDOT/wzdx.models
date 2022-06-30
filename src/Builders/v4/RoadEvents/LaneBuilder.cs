using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.RoadEvents
{
    /// <summary>
    /// Provides a builder for a v4 Lane class
    /// </summary>
    public class LaneBuilder : IBuilder<Lane>
    {
        private BuilderConfiguration<Lane> Configuration { get; }
            = new BuilderConfiguration<Lane>();

        public LaneBuilder(LaneType type, LaneStatus status, int order)
        {
            WithType(type);
            WithStatus(status);
            WithOrder(order);
        }

        public LaneBuilder WithType(LaneType value)
        {
            Configuration.Set(lane => lane.Type, value);
            return this;
        }

        public LaneBuilder WithStatus(LaneStatus value)
        {
            Configuration.Set(lane => lane.Status, value);
            return this;
        }

        public LaneBuilder WithOrder(int value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), value, "Order must be greater than zero.");

            Configuration.Set(lane => lane.Order, value);
            return this;
        }

        public LaneBuilder WithRestriction(RestrictionType type, UnitOfMeasurement unit, Func<RestrictionBuilder, RestrictionBuilder> configure)
        {
            var restriction = configure(new RestrictionBuilder(type, unit)).Result();
            Configuration.Set(lane => lane.Restrictions, lane =>
            {
                if (lane.Restrictions == null)
                    lane.Restrictions = new List<Restriction>();
                lane.Restrictions.Add(restriction);
            });
            return this;
        }

        public LaneBuilder WithNoRestrictions()
        {
            Configuration.Set(lane => lane.Restrictions, (object)null);
            return this;
        }

        [Pure]
        public Lane Result()
        {
            var result = new Lane();
            Configuration.ApplyTo(result);
            return result;
        }
    }
}