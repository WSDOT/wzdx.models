using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.Builders
{
    public class LaneBuilder
    {
        private LaneType _type;
        private LaneStatus _status;
        private int _order;
        private ICollection<RestrictionBuilder> _restrictionBuilders = new List<RestrictionBuilder>();

        public LaneBuilder(LaneType type, LaneStatus status, int order)
        {
            if (order <= 0)
                throw new ArgumentOutOfRangeException(nameof(order), order, "Order must be greater than zero.");

            _type = type;
            _status = status;
            _order = order;
        }

        public Lane Result()
        {
            return new Lane()
            {
                Type = _type,
                Status = _status,
                Order = _order,
                LaneNumber = null,
                Restrictions = _restrictionBuilders.Select(builder => builder.Result()).ToList()
            };
        }

        public LaneBuilder WithType(LaneType value)
        {
            _type = value;
            return this;
        }

        public LaneBuilder WithStatus(LaneStatus value)
        {
            _status = value;
            return this;
        }

        public LaneBuilder WithOrder(int value)
        {
            _order = value;
            return this;
        }

        public LaneBuilder WithRestriction(RestrictionType type, UnitOfMeasurement unit, Action<RestrictionBuilder> configure)
        {
            var builder = new RestrictionBuilder(type, unit);
            configure(builder);
            _restrictionBuilders.Add(builder);
            return this;
        }
    }
}