using System;
using System.Linq;
using Wzdx.Core;
using Wzdx.GeoJson.Geometries;
using Wzdx.v4.WorkZones;

namespace Wzdx.v4.RoadEvents
{
    /// <summary>
    /// Provides a builder for a v4 RoadEventFeature (Detour) class
    /// </summary>
    public sealed class DetourRoadEventFeatureBuilder : RoadEventFeatureBuilder<DetourRoadEventFeatureBuilder, DetourRoadEvent>
    {
        public DetourRoadEventFeatureBuilder(string sourceId, string featureId, string roadName, Direction direction) :
            base(new DelegatingFactory<RoadEventFeature>(() => new RoadEventFeature() { Properties = new DetourRoadEvent() }))
        {
            FeatureConfiguration.Set(feature => feature.Id, featureId);
            CoreDetailConfiguration.Set(details => details.DataSourceId, sourceId);
            WithGeometry(MultiPoint.FromCoordinates(Enumerable.Empty<Position>()));
            WithRoadName(roadName);
            WithDirection(direction);
            WithStatus(EventStatus.Pending);
            WithStart(DateTimeOffset.MinValue, TimeVerification.Estimated);
            WithEnd(DateTimeOffset.MinValue, TimeVerification.Estimated);
        }

        public DetourRoadEventFeatureBuilder WithBeginning(double milepost)
        {
            PropertiesConfiguration.Set(properties => properties.BeginningMilepost, milepost);
            PropertiesConfiguration.Default(properties => properties.BeginningCrossStreet);
            return Derived();
        }

        public DetourRoadEventFeatureBuilder WithBeginning(string crossStreet)
        {
            if (string.IsNullOrEmpty(crossStreet))
                throw new ArgumentException("Value cannot be null or empty.", nameof(crossStreet));

            PropertiesConfiguration.Default(properties => properties.BeginningMilepost);
            PropertiesConfiguration.Set(properties => properties.BeginningCrossStreet, crossStreet);
            return Derived();
        }

        public DetourRoadEventFeatureBuilder WithEnding(double milepost)
        {
            PropertiesConfiguration.Set(properties => properties.EndingMilepost, milepost);
            PropertiesConfiguration.Default(properties => properties.EndingCrossStreet);
            return Derived();
        }

        public DetourRoadEventFeatureBuilder WithEnding(string crossStreet)
        {
            if (string.IsNullOrEmpty(crossStreet))
                throw new ArgumentException("Value cannot be null or empty.", nameof(crossStreet));

            PropertiesConfiguration.Default(properties => properties.EndingMilepost);
            PropertiesConfiguration.Set(properties => properties.EndingCrossStreet, crossStreet);
            return Derived();
        }

        public DetourRoadEventFeatureBuilder WithStatus(EventStatus value)
        {
            PropertiesConfiguration.Set(properties => properties.EventStatus, value);
            return Derived();
        }
        public DetourRoadEventFeatureBuilder WithStart(DateTimeOffset value, TimeVerification accuracy)
        {
            PropertiesConfiguration.Set(properties => properties.StartDate, value);
            PropertiesConfiguration.Set(properties => properties.StartDateAccuracy, accuracy);
            return Derived();
        }

        public DetourRoadEventFeatureBuilder WithEnd(DateTimeOffset value, TimeVerification accuracy)
        {
            PropertiesConfiguration.Set(properties => properties.EndDate, value);
            PropertiesConfiguration.Set(properties => properties.EndDateAccuracy, accuracy);
            return Derived();
        }
    }
}