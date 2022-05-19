using System;
using System.Collections.Generic;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.Builders
{
    public class DetourRoadEventBuilder : IBuilder<DetourRoadEvent>
    {
        private readonly string _sourceId;
        private Direction _direction;
        private readonly ICollection<string> _roadNames = new List<string>();
        private string _description;
        private DateTimeOffset _updateDate = DateTimeOffset.UtcNow;
        private DateTimeOffset _createDate = DateTimeOffset.UtcNow;
        private Relationship _relationship;
        private TimeVerification _startDateAccuracy = TimeVerification.Estimated;
        private TimeVerification _endDateAccuracy = TimeVerification.Estimated;
        private EventStatus _eventStatus = EventStatus.Pending;

        public DetourRoadEventBuilder(string sourceId, string roadName, Direction direction)
        {
            _sourceId = sourceId;
            _roadNames.Add(roadName);
            _direction = direction;
            _description = null;
        }

        // ReSharper disable once UnusedMember.Global
        public DetourRoadEventBuilder WithRoadName(string value)
        {
            _roadNames.Add(value);
            return this;
        }

        // ReSharper disable once UnusedMember.Global
        public DetourRoadEventBuilder WithDirection(Direction direction)
        {
            _direction = direction;
            return this;
        }

        // ReSharper disable once UnusedMember.Global
        public DetourRoadEventBuilder WithDescription(string value)
        {
            _description = value;
            return this;
        }

        // ReSharper disable once UnusedMember.Global
        public DetourRoadEventBuilder WithRelationship(Relationship value)
        {
            _relationship = value;
            return this;
        }

        // ReSharper disable once UnusedMember.Global
        public DetourRoadEventBuilder WithCreated(DateTimeOffset value)
        {
            _createDate = value;
            return this;
        }

        // ReSharper disable once UnusedMember.Global
        public DetourRoadEventBuilder WithUpdated(DateTimeOffset value)
        {
            _updateDate = value;
            return this;
        }
        
        public DetourRoadEvent Result()
        {
            return new DetourRoadEvent()
            {
                CoreDetails = new RoadEventCoreDetails()
                {
                    DataSourceId = _sourceId,
                    EventType = EventType.Detour,
                    RoadNames = _roadNames,
                    Direction = _direction,
                    Description = _description,
                    Relationship = _relationship,
                    CreationDate = _createDate,
                    UpdateDate = _updateDate
                },
                //todo DetourRoadEventBuilder.WithBeginningCrossStreet = ,
                //todo DetourRoadEventBuilder.WithBeginningMilepost = ,
                //todo DetourRoadEventBuilder.WithEndDate = ,
                EndDateAccuracy = _endDateAccuracy,
                //todo DetourRoadEventBuilder.WithEndingCrossStreet = ,
                //todo DetourRoadEventBuilder.WithEndingMilepost = ,
                //todo DetourRoadEventBuilder.WithEventStatus = ,
                EventStatus = _eventStatus,
                //todo DetourRoadEventBuilder.WithStartDate = ,
                StartDateAccuracy = _startDateAccuracy,
                //todo DetourRoadEventBuilder.WithAdditionalProperties = ,
            };
        }
    }
}