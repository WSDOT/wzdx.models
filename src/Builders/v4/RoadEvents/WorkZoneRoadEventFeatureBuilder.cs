using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Wzdx.Core;
using Wzdx.GeoJson.Geometries;
using Wzdx.v4.WorkZones;

namespace Wzdx.v4.RoadEvents
{
    /// <summary>
    /// Provides a builder for a v4 RoadEventFeature (WorkZone) class
    /// </summary>
    public sealed class WorkZoneRoadEventFeatureBuilder : 
        RoadEventFeatureBuilder<WorkZoneRoadEventFeatureBuilder, WorkZoneRoadEvent>
    {
        public WorkZoneRoadEventFeatureBuilder(string sourceId, string featureId, string roadName, Direction direction)
            : base(new DelegatingFactory<RoadEventFeature>(() => new RoadEventFeature() { Properties = new WorkZoneRoadEvent() }))
        {
            FeatureConfiguration.Set(feature => feature.Id, featureId);
            CoreDetailConfiguration.Set(details => details.DataSourceId, sourceId);
            //CoreDetailConfiguration.Set(details => details.Name, featureId);    // todo: set name!

            WithGeometry(MultiPoint.FromCoordinates(Enumerable.Empty<Position>()));
            WithRoadName(roadName);
            WithDirection(direction);
            WithStatus(EventStatus.Pending);
            WithBeginning(SpatialVerification.Estimated);
            WithEnding(SpatialVerification.Estimated);
            WithStart(DateTimeOffset.MinValue, TimeVerification.Estimated);
            WithEnd(DateTimeOffset.MinValue, TimeVerification.Estimated);   
            WithLocationMethod(LocationMethod.Unknown);
            WithVehicleImpact(VehicleImpact.Unknown);
        }

        public WorkZoneRoadEventFeatureBuilder WithLocationMethod(LocationMethod value)
        {
            PropertiesConfiguration.Set(properties => properties.LocationMethod, value);
            return Derived();
        }

        public WorkZoneRoadEventFeatureBuilder WithBeginning(double milepost, SpatialVerification verification)
        {
            PropertiesConfiguration.Set(properties => properties.BeginningMilepost, milepost);
            PropertiesConfiguration.Default(properties => properties.BeginningCrossStreet);
            PropertiesConfiguration.Set(properties => properties.BeginningAccuracy, verification);

            return Derived();
        }

        public WorkZoneRoadEventFeatureBuilder WithBeginning(SpatialVerification verification)
        {
            PropertiesConfiguration.Set(properties => properties.BeginningAccuracy, verification);
            return Derived();
        }

        public WorkZoneRoadEventFeatureBuilder WithBeginning(string crossStreet, SpatialVerification verification)
        {
            if (string.IsNullOrEmpty(crossStreet))
                throw new ArgumentException("Value cannot be null or empty.", nameof(crossStreet));

            PropertiesConfiguration.Default(properties => properties.BeginningMilepost);
            PropertiesConfiguration.Set(properties => properties.BeginningCrossStreet, crossStreet);
            PropertiesConfiguration.Set(properties => properties.BeginningAccuracy, verification);
            return Derived();
        }

        public WorkZoneRoadEventFeatureBuilder WithEnding(SpatialVerification verification)
        {
            PropertiesConfiguration.Set(properties => properties.EndingAccuracy, verification);
            return Derived();
        }

        public WorkZoneRoadEventFeatureBuilder WithEnding(double milepost, SpatialVerification verification)
        {
            PropertiesConfiguration.Set(properties => properties.EndingMilepost, milepost);
            PropertiesConfiguration.Default(properties => properties.EndingCrossStreet);
            PropertiesConfiguration.Set(properties => properties.EndingAccuracy, verification);

            return Derived();
        }

        public WorkZoneRoadEventFeatureBuilder WithEnding(string crossStreet, SpatialVerification verification)
        {
            if (string.IsNullOrEmpty(crossStreet))
                throw new ArgumentException("Value cannot be null or empty.", nameof(crossStreet));

            PropertiesConfiguration.Default(properties => properties.EndingMilepost);
            PropertiesConfiguration.Set(properties => properties.EndingCrossStreet, crossStreet);
            PropertiesConfiguration.Set(properties => properties.EndingAccuracy, verification);
            return Derived();
        }

        public WorkZoneRoadEventFeatureBuilder WithStatus(EventStatus value)
        {
            PropertiesConfiguration.Set(properties => properties.EventStatus, value);
            return Derived();
        }

        public WorkZoneRoadEventFeatureBuilder WithReducedSpeedLimitKph(double value)
        {
            PropertiesConfiguration.Set(properties => properties.ReducedSpeedLimitKph, value);
            return Derived();
        }

        public WorkZoneRoadEventFeatureBuilder WithNoReducedSpeedLimitKph()
        {
            PropertiesConfiguration.Default(properties => properties.ReducedSpeedLimitKph);
            return Derived();
        }

        public WorkZoneRoadEventFeatureBuilder WithTypesOfWork(IEnumerable<TypeOfWork> value)
        {
            PropertiesConfiguration.Set(properties => properties.TypesOfWork, value.ToList());
            return Derived();
        }

        public WorkZoneRoadEventFeatureBuilder WithVehicleImpact(VehicleImpact value)
        {
            PropertiesConfiguration.Set(properties => properties.VehicleImpact, value);
            return Derived();
        }

        public WorkZoneRoadEventFeatureBuilder WithWorkerPresence(Func<WorkerPresenceBuilder, WorkerPresenceBuilder> config)
        {
            var presence = config(new WorkerPresenceBuilder(false)).Result();
            PropertiesConfiguration.Set(properties => properties.WorkerPresence, presence);
            return Derived();
        }

        public WorkZoneRoadEventFeatureBuilder WithStart(DateTimeOffset value, TimeVerification accuracy)
        {
            PropertiesConfiguration.Set(properties => properties.StartDate, value);
            PropertiesConfiguration.Set(properties => properties.StartDateAccuracy, accuracy);
            return Derived();
        }

        public WorkZoneRoadEventFeatureBuilder WithEnd(DateTimeOffset value, TimeVerification accuracy)
        {
            PropertiesConfiguration.Set(properties => properties.EndDate, value);
            PropertiesConfiguration.Set(properties => properties.EndDateAccuracy, accuracy);
            return Derived();
        }

        public WorkZoneRoadEventFeatureBuilder WithLane(LaneType type, LaneStatus status, int order, Func<LaneBuilder, LaneBuilder> configure)
        {
            var builder = configure(new LaneBuilder(type, status, order));
            var lane = builder.Result();
            PropertiesConfiguration.Combine(properties => properties.Lanes, properties => properties.Lanes.Add(lane));
            return Derived();
        }

        public WorkZoneRoadEventFeatureBuilder WithRestriction(RestrictionType type, UnitOfMeasurement unit, Func<RestrictionBuilder, RestrictionBuilder> configure)
        {
            var builder = configure(new RestrictionBuilder(type, unit));
            var restriction = builder.Result();
            PropertiesConfiguration.Combine(properties => properties.Restrictions, properties => properties.Restrictions.Add(restriction));
            return Derived();
        }

        [Pure]
        public override RoadEventFeature Result()
        {
            var result = base.Result();
            PropertiesConfiguration.ApplyTo((WorkZoneRoadEvent)result.Properties);
            return result;
        }
    }
}