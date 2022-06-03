using System;
using System.Collections.Generic;
using Wsdot.Wzdx.Core;

namespace Wsdot.Wzdx.v4.Devices
{
    public sealed class MarkedLocationBuilder : Builder<MarkedLocation>
    {
        public MarkedLocationBuilder() : this(new List<Action<MarkedLocation>>(),
            location =>
            {
                location.Type = MarkedLocationType.TemporaryTrafficSignal; 
            })
        {

        }

        private MarkedLocationBuilder(IEnumerable<Action<MarkedLocation>> configuration, Action<MarkedLocation> step) : base(configuration, step)
        {

        }

        public MarkedLocationBuilder WithType(MarkedLocationType value)
        {
            return new MarkedLocationBuilder(Configuration, location => location.Type = value);
        }

        public MarkedLocationBuilder WithRoadEvent(string value)
        {
            return new MarkedLocationBuilder(Configuration, location => location.RoadEventId = value);
        }

        protected override Func<MarkedLocation> ResultFactory { get; } = () => new MarkedLocation();
    }
}