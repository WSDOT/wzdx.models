using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.GeoJson.Geometries;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.RoadEvents
{

    public sealed class WorkZoneRoadEventFeatureBuilder : RoadEventFeatureBuilder<WorkZoneRoadEventFeatureBuilder, WorkZoneRoadEvent>
    {
        public WorkZoneRoadEventFeatureBuilder(string sourceId, string featureId, string roadName, Direction direction)
            : this(new List<Action<RoadEventFeature>>(), (feature, workZone) =>
            {
                var geometry = MultiPoint.FromCoordinates(Enumerable.Empty<Position>());
                feature.Id = featureId;
                feature.Geometry = geometry;
                feature.BoundaryBox = geometry.BoundaryBox.ToList().AsReadOnly();
                feature.Properties.CoreDetails.DataSourceId = sourceId;
                feature.Properties.CoreDetails.RoadNames.Add(roadName);
                feature.Properties.CoreDetails.Direction = direction;

                workZone.LocationMethod = LocationMethod.Unknown;
                workZone.VehicleImpact = VehicleImpact.Unknown;
                workZone.EventStatus = EventStatus.Pending;
                workZone.StartDateAccuracy = TimeVerification.Estimated;
                workZone.EndDateAccuracy = TimeVerification.Estimated;
                workZone.BeginningAccuracy = SpatialVerification.Estimated;
                workZone.EndingAccuracy = SpatialVerification.Estimated;
            })
        {

        }

        private WorkZoneRoadEventFeatureBuilder(IEnumerable<Action<RoadEventFeature>> configuration, Action<RoadEventFeature, WorkZoneRoadEvent> step) : base(configuration, step)
        {
            // ignore
        }

        public WorkZoneRoadEventFeatureBuilder WithLocationMethod(LocationMethod value)
        {
            return CreateWith((_, workZone) => workZone.LocationMethod = value );
        }
        
        public WorkZoneRoadEventFeatureBuilder WithBeginning(double milepost, SpatialVerification verification)
        {
            return CreateWith((_, workZone) =>
            {
                workZone.BeginningMilepost = milepost;
                workZone.BeginningCrossStreet = string.Empty;
                workZone.BeginningAccuracy = verification;
            });
        }

        public WorkZoneRoadEventFeatureBuilder WithBeginning(string crossStreet, SpatialVerification verification)
        {
            return CreateWith((_, workZone) =>
            {
                workZone.BeginningMilepost = 0;
                workZone.BeginningCrossStreet = crossStreet;
                workZone.BeginningAccuracy = verification;
            });
        }
        
        public WorkZoneRoadEventFeatureBuilder WithEnding(double milepost, SpatialVerification verification)
        {
            return CreateWith((_, workZone) =>
            {
                workZone.EndingMilepost = milepost;
                workZone.EndingCrossStreet = string.Empty;
                workZone.EndingAccuracy = verification;
            });
        }

        public WorkZoneRoadEventFeatureBuilder WithEnding(string crossStreet, SpatialVerification verification)
        {
            return CreateWith((_, workZone) =>
            {
                workZone.EndingMilepost = 0;
                workZone.EndingCrossStreet = crossStreet;
                workZone.EndingAccuracy = verification;
            });
        }

        public WorkZoneRoadEventFeatureBuilder WithStatus(EventStatus value)
        {
            return CreateWith((_, workZone) => workZone.EventStatus = value );
        }

        public WorkZoneRoadEventFeatureBuilder WithReducedSpeedLimitKph(double value)
        {
            return CreateWith((_, workZone) => workZone.ReducedSpeedLimitKph = value);
        }

        public WorkZoneRoadEventFeatureBuilder WithNoReducedSpeedLimitKph()
        {
            return CreateWith((_, workZone) => workZone.ReducedSpeedLimitKph = null);
        }

        public WorkZoneRoadEventFeatureBuilder WithTypesOfWork(IEnumerable<TypeOfWork> value)
        {
            return CreateWith((_, workZone) => workZone.TypesOfWork = value.ToList());
        }

        public WorkZoneRoadEventFeatureBuilder WithVehicleImpact(VehicleImpact value)
        {
            return CreateWith((_, workZone) => workZone.VehicleImpact = value);
        }

        public WorkZoneRoadEventFeatureBuilder WithWorkerPresence(Func<WorkerPresenceBuilder, WorkerPresenceBuilder> config)
        {
            var presence = config(new WorkerPresenceBuilder(false)).Result();
            return CreateWith((_, workZone) => workZone.WorkerPresence = presence);
        }
        
        public WorkZoneRoadEventFeatureBuilder WithStart(DateTimeOffset value, TimeVerification accuracy)
        {
            return CreateWith((_, workZone) =>
            {
                workZone.StartDate = value;
                workZone.StartDateAccuracy = accuracy;
            });
        }

        public WorkZoneRoadEventFeatureBuilder WithEnd(DateTimeOffset value, TimeVerification accuracy)
        {
            return CreateWith((_, workZone) =>
            {
                workZone.EndDate = value;
                workZone.EndDateAccuracy = accuracy;
            });
        }

        // ReSharper disable once UnusedMember.Global
        public WorkZoneRoadEventFeatureBuilder WithLane(LaneType type, LaneStatus status, int order, Action<LaneBuilder> configure)
        {
            var builder = new LaneBuilder(type, status, order);
            configure(builder);
            var lane = builder.Result();
            return CreateWith((_, workZone) => workZone.Lanes.Add(lane));
        }
        
        // ReSharper disable once UnusedMember.Global
        public WorkZoneRoadEventFeatureBuilder WithRestriction(RestrictionType type, UnitOfMeasurement unit, Action<RestrictionBuilder> configure)
        {
            var builder = new RestrictionBuilder(type, unit);
            configure(builder);
            var lane = builder.Result();
            return CreateWith((_, workZone) => workZone.Restrictions.Add(lane));
        }

        protected override WorkZoneRoadEventFeatureBuilder CreateWith(Action<RoadEventFeature, WorkZoneRoadEvent> step)
        {
            return new WorkZoneRoadEventFeatureBuilder(Configuration, step);
        }

        protected override Func<WorkZoneRoadEvent> ResultProperties { get; } = () =>
            new WorkZoneRoadEvent();
    }
}