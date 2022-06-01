using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.Builders
{
    public class RoadRestrictionEventBuilder : 
        IBuilder<RestrictionRoadEvent>
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
        
        public RoadRestrictionEventBuilder(string sourceId, string roadName, Direction direction)
        {
            _sourceId = sourceId;
            _roadNames.Add(roadName);
            _direction = direction;
            _description = null;
        }

        // ReSharper disable once UnusedMember.Global
        public RoadRestrictionEventBuilder WithRoadName(string value)
        {
            _roadNames.Add(value);
            return this;
        }

        // ReSharper disable once UnusedMember.Global
        public RoadRestrictionEventBuilder WithDirection(Direction direction)
        {
            _direction = direction;
            return this;
        }

        // ReSharper disable once UnusedMember.Global
        public RoadRestrictionEventBuilder WithDescription(string value)
        {
            _description = value;
            return this;
        }

        // ReSharper disable once UnusedMember.Global
        public RoadRestrictionEventBuilder WithRelationship(Relationship value)
        {
            _relationship = value;
            return this;
        }

        // ReSharper disable once UnusedMember.Global
        public RoadRestrictionEventBuilder WithCreated(DateTimeOffset value)
        {
            _createDate = value;
            return this;
        }

        // ReSharper disable once UnusedMember.Global
        public RoadRestrictionEventBuilder WithUpdated(DateTimeOffset value)
        {
            _updateDate = value;
            return this;
        }

        // ReSharper disable once UnusedMember.Global
        public RoadRestrictionEventBuilder WithLane(LaneType type, LaneStatus status, int order, Action<LaneBuilder> configure)
        {
            var builder = new LaneBuilder(type, status, order);
            configure(builder);
            _laneBuilders.Add(builder);
            return this;
        }

        // ReSharper disable once UnusedMember.Global
        public RoadRestrictionEventBuilder WithRestriction(RestrictionType type, UnitOfMeasurement unit, Action<RestrictionBuilder> configure)
        {
            var builder = new RestrictionBuilder(type, unit);
            configure(builder);
            _restrictionBuilders.Add(builder);
            return this;
        }

        public RestrictionRoadEvent Result()
        {
            return new RestrictionRoadEvent()
            {
                CoreDetails = new RoadEventCoreDetails()
                {
                    DataSourceId = _sourceId,
                    EventType = EventType.Restriction,
                    RoadNames = _roadNames,
                    Direction = _direction,
                    Description = _description,
                    Relationship = _relationship,
                    CreationDate = _createDate,
                    UpdateDate = _updateDate
                },
                Restrictions = _restrictionBuilders.Select(builder => builder.Result()).ToList(),
                Lanes = _laneBuilders.Select(builder => builder.Result()).ToList(),
                //todo RestrictionRoadEventBuilder.WithAdditionalProperties = ,
            };
        }
    }
}