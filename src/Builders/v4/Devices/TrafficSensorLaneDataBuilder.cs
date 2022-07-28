using System;
using Wzdx.Core;

namespace Wzdx.v4.Devices
{
    /// <summary>
    /// Provides a builder for a v4 TrafficSensorLaneData class
    /// </summary>
    public sealed class TrafficSensorLaneDataBuilder : IBuilder<TrafficSensorLaneData>
    {
        private BuilderConfiguration<TrafficSensorLaneData> Configuration { get; }
            = new BuilderConfiguration<TrafficSensorLaneData>();

        public TrafficSensorLaneDataBuilder()
        {
            WithRoadEvent(string.Empty);
            WithLaneOrder(1);
        }

        public TrafficSensorLaneDataBuilder WithRoadEvent(string value)
        {
            Configuration.Set(properties => properties.RoadEventId, value ?? string.Empty);
            return this;
        }

        public TrafficSensorLaneDataBuilder WithLaneOrder(int value)
        {
            if (value < 1) throw new ArgumentOutOfRangeException(nameof(value), value, "Lane order value must be greater than 0");

            Configuration.Set(properties => properties.AverageSpeedKph, value);
            return this;
        }

        public TrafficSensorLaneDataBuilder WithAverageSpeedKph(int value)
        {
            Configuration.Set(properties => properties.AverageSpeedKph, value);
            return this;
        }

        public TrafficSensorLaneDataBuilder WithOccupancyPercent(int value)
        {
            Configuration.Set(properties => properties.OccupancyPercent, value);
            return this;
        }

        public TrafficSensorLaneDataBuilder WithVolumeVph(int value)
        {
            Configuration.Set(properties => properties.VolumeVph, value);
            return this;
        }

        public TrafficSensorLaneData Result()
        {
            var result = new TrafficSensorLaneData();
            Configuration.ApplyTo(result);
            return result;
        }
    }
}