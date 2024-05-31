using System;
using System.Linq;
using Wzdx.Core;
using Wzdx.GeoJson.Geometries;
using Wzdx.v4.WorkZones;

namespace Wzdx.v4.RoadEvents
{
    /// <summary>
    /// Provides a builder for a v4 RoadEventFeature (Incident) class
    /// </summary>
    public sealed class RoadIncidentFeatureBuilder : RoadEventFeatureBuilder<RoadIncidentFeatureBuilder, IncidentRoadEvent>
    {
        public RoadIncidentFeatureBuilder(string sourceId, string featureId, string roadName, Direction direction) :
            base(new DelegatingFactory<RoadEventFeature>(() => new RoadEventFeature() { Properties = new IncidentRoadEvent() }))
        {
            FeatureConfiguration.Set(feature => feature.Id, featureId);
            CoreDetailConfiguration.Set(details => details.DataSourceId, sourceId);
            WithVehicleImpact(VehicleImpact.Unknown);
            WithGeometry(MultiPoint.FromCoordinates(Enumerable.Empty<Position>()));
            WithRoadName(roadName);
            WithDirection(direction);
        }

        public RoadIncidentFeatureBuilder WithStart(DateTimeOffset value)
        {
            return WithStart(value, false);
        }

        public RoadIncidentFeatureBuilder WithStart(DateTimeOffset value, bool verified)
        {
            PropertiesConfiguration.Set(properties => properties.StartDate, value.ToLocalTime());
            PropertiesConfiguration.Set(properties => properties.IsStartDateVerified, verified);
            return Derived();
        }

        public RoadIncidentFeatureBuilder WithEnd(DateTimeOffset? value)
        {
            return WithEnd(value, false);
        }

        public RoadIncidentFeatureBuilder WithEnd(DateTimeOffset? value, bool verified)
        {
            if (value == null)
            {
                return WithoutEnd();
            }
            
            PropertiesConfiguration.Set(properties => properties.EndDate, value.Value.ToLocalTime());
            PropertiesConfiguration.Set(properties => properties.IsEndDateVerified, verified);

            return Derived();
        }

        public RoadIncidentFeatureBuilder WithoutEnd()
        {
            PropertiesConfiguration.Set(properties => properties.EndDate, (DateTimeOffset?)null);
            PropertiesConfiguration.Set(properties => properties.IsEndDateVerified, (bool?)null);
            return Derived();
        }


        public RoadIncidentFeatureBuilder WithRestriction(RestrictionType type)
        {
            return WithRestriction(type, configure => configure);
        }

        public RoadIncidentFeatureBuilder WithRestriction(RestrictionType type, Func<RestrictionBuilder, RestrictionBuilder> configure)
        {
            var builder = configure(new RestrictionBuilder(type));
            var value = builder.Result();
            PropertiesConfiguration.Combine(properties => properties.Restrictions, properties => properties.Restrictions.Add(value));
            return Derived();
        }

        public RoadIncidentFeatureBuilder WithLane(LaneType type, LaneStatus status, int order)
        {
            return WithLane(type, status, order, item => item);
        }

        public RoadIncidentFeatureBuilder WithLane(LaneType type, LaneStatus status, int order, Func<LaneBuilder, LaneBuilder> configure)
        {
            var builder = configure(new LaneBuilder(type, status, order));
            var value = builder.Result();
            PropertiesConfiguration.Combine(properties => properties.Lanes, properties => properties.Lanes.Add(value));
            return Derived();
        }

        public RoadIncidentFeatureBuilder WithIncident(IncidentCategory category, IncidentType type, string description)
        {
            var value = new TypeOfIncident()
            {
                Description = description,
                IncidentCategory = category,
                IncidentType = type
            };

            PropertiesConfiguration.Combine(properties => properties.TypesOfIncident, properties => properties.TypesOfIncident.Add(value));
            return Derived();
        }

        public RoadIncidentFeatureBuilder WithVehicleImpact(VehicleImpact value)
        {
            PropertiesConfiguration.Set(properties => properties.VehicleImpact, value);
            return Derived();
        }
    }
}