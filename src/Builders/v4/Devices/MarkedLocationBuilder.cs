using Wzdx.Core;

namespace Wzdx.v4.Devices
{
    /// <summary>
    /// Provides a builder for a v4 MarkedLocation class
    /// </summary>
    public class MarkedLocationBuilder : IBuilder<MarkedLocation>
    {
        private BuilderConfiguration<MarkedLocation> Configuration { get; }
            = new BuilderConfiguration<MarkedLocation>();

        public MarkedLocationBuilder(MarkedLocationType type) 
        {
            WithType(type);
        }
        
        public MarkedLocationBuilder WithType(MarkedLocationType value)
        {
            Configuration.Set(location => location.Type, value);
            return this;
        }
        
        public MarkedLocationBuilder WithRoadEvent(string value)
        {
            Configuration.Set(location => location.RoadEventId, value);
            return this;
        }

        public MarkedLocation Result()
        {
            var result = new MarkedLocation();
            Configuration.ApplyTo(result);
            return result;
        }
    }
}