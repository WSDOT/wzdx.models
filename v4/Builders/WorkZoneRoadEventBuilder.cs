using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.Builders
{
    public class WorkZoneRoadEventBuilder : IBuilder<WorkZoneRoadEvent>
    {
        private readonly string _sourceId;
        private Direction _direction;
        private readonly ICollection<string> _roadNames = new HashSet<string>();
        private readonly ICollection<LaneBuilder> _laneBuilders = new List<LaneBuilder>();
        private readonly ICollection<RestrictionBuilder> _restrictionBuilders = new List<RestrictionBuilder>();
        private string _description;
        private DateTimeOffset _updateDate = DateTimeOffset.UtcNow;
        private DateTimeOffset _createDate = DateTimeOffset.UtcNow;
        private Relationship _relationship;
        private DateTimeOffset? _startDate;
        private DateTimeOffset? _endDate;
        private TimeVerification _startDateAccuracy = TimeVerification.Estimated;
        private TimeVerification _endDateAccuracy = TimeVerification.Estimated;
        private EventStatus _eventStatus = EventStatus.Pending;

        public WorkZoneRoadEventBuilder(string sourceId, string roadName, Direction direction)
        {
            _sourceId = sourceId;
            _roadNames.Add(roadName);
            _direction = direction;
            _description = null;
        }

        // ReSharper disable once UnusedMember.Global
        public WorkZoneRoadEventBuilder WithRoadName(string value)
        {
            _roadNames.Add(value);
            return this;
        }

        // ReSharper disable once UnusedMember.Global
        public WorkZoneRoadEventBuilder WithDirection(Direction direction)
        {
            _direction = direction;
            return this;
        }

        // ReSharper disable once UnusedMember.Global
        public WorkZoneRoadEventBuilder WithDescription(string value)
        {
            _description = value;
            return this;
        }

        // ReSharper disable once UnusedMember.Global
        public WorkZoneRoadEventBuilder WithRelationship(Relationship value)
        {
            _relationship = value;
            return this;
        }

        // ReSharper disable once UnusedMember.Global
        public WorkZoneRoadEventBuilder WithCreated(DateTimeOffset value)
        {
            _createDate = value;
            return this;
        }

        // ReSharper disable once UnusedMember.Global
        public WorkZoneRoadEventBuilder WithUpdated(DateTimeOffset value)
        {
            _updateDate = value;
            return this;
        }

        // optional
        public WorkZoneRoadEventBuilder WithStart(DateTimeOffset value, TimeVerification accuracy)
        {
            _startDate = value;
            _startDateAccuracy = accuracy;
            return this;
        }

        // optional
        public WorkZoneRoadEventBuilder WithEnd(DateTimeOffset value, TimeVerification accuracy)
        {
            _endDate = value;
            _endDateAccuracy = accuracy;
            return this;
        }



        // ReSharper disable once UnusedMember.Global
        public WorkZoneRoadEventBuilder WithLane(LaneType type, LaneStatus status, int order, Action<LaneBuilder> configure)
        {
            var builder = new LaneBuilder(type, status, order);
            configure(builder);
            _laneBuilders.Add(builder);
            return this;
        }

        // ReSharper disable once UnusedMember.Global
        public WorkZoneRoadEventBuilder WithRestriction(RestrictionType type, UnitOfMeasurement unit, Action<RestrictionBuilder> configure)
        {
            var builder = new RestrictionBuilder(type, unit);
            configure(builder);
            _restrictionBuilders.Add(builder);
            return this;
        }

        public WorkZoneRoadEvent Result()
        {
            var result = new WorkZoneRoadEvent()
            {
                CoreDetails = new RoadEventCoreDetails()
                {
                    DataSourceId = _sourceId,
                    EventType = EventType.WorkZone,
                    RoadNames = _roadNames,
                    Direction = _direction,
                    Description = _description,
                    Relationship = _relationship,
                    CreationDate = _createDate,
                    UpdateDate = _updateDate
                },
                Restrictions = _restrictionBuilders.Select(builder => builder.Result()).ToList(),
                Lanes = _laneBuilders.Select(builder => builder.Result()).ToList(),
                //todo WorkZoneRoadEventBuilder.WithBeginningMilepost = ,
                //todo WorkZoneRoadEventBuilder.WithEndDate = ,
                EndDateAccuracy = _endDateAccuracy,
                //todo WorkZoneRoadEventBuilder.WithEndingCrossStreet = ,
                EndingCrossStreet = string.Empty,
                //todo WorkZoneRoadEventBuilder.WithEndingMilepost = ,
                //todo WorkZoneRoadEventBuilder.WithEventStatus = ,
                EventStatus = _eventStatus,
                //todo WorkZoneRoadEventBuilder.WithStartDate = ,
                StartDateAccuracy = _startDateAccuracy,
                //todo WorkZoneRoadEventBuilder.WithBeginningAccuracy = ,
                BeginningAccuracy = SpatialVerification.Estimated,
                //todo WorkZoneRoadEventBuilder.WithBeginningCrossStreet = ,
                BeginningCrossStreet = string.Empty,
                //todo WorkZoneRoadEventBuilder.WithEndingAccuracy = ,
                EndingAccuracy = SpatialVerification.Estimated,
                //todo WorkZoneRoadEventBuilder.WithLocationMethod = ,
                LocationMethod = LocationMethod.Unknown,
                //todo WorkZoneRoadEventBuilder.WithReducedSpeedLimitKph = ,
                //todo WorkZoneRoadEventBuilder.WithTypesOfWork = ,
                //todo WorkZoneRoadEventBuilder.WithVehicleImpact = ,
                VehicleImpact = VehicleImpact.Unknown,
                //todo WorkZoneRoadEventBuilder.WithWorkerPresence = ,
                //todo WorkZoneRoadEventBuilder.WithAdditionalProperties = ,
            };

            if (_startDate.HasValue)
            {
                result.StartDate = _startDate.Value;
            }

            if (_endDate.HasValue)
            {
                result.EndDate = _endDate.Value;
            }

            return result;
        }
    }
}