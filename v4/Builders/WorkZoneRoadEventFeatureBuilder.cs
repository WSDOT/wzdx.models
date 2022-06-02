using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.GeoJson.Geometries;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.Builders
{

    public class WorkZoneRoadEventFeatureBuilder : IBuilder<RoadEventFeature>
    {
        private readonly string _featureId;
        private IGeometry _geometry;
        private IEnumerable<double> _boundingBox;
        private WorkZoneRoadEventBuilder _eventBuilder;

        public WorkZoneRoadEventFeatureBuilder(string sourceId, string featureId, string roadName, Direction direction)
            : this(featureId, new WorkZoneRoadEventBuilder(sourceId, roadName, direction))
        {

        }

        private WorkZoneRoadEventFeatureBuilder(string featureId, WorkZoneRoadEventBuilder eventBuilder)
        {
            _featureId = featureId;
            _eventBuilder = eventBuilder;
            _geometry = MultiPoint.FromCoordinates(Enumerable.Empty<Position>());
        }

        public WorkZoneRoadEventFeatureBuilder WithFeatureId(string value)
        {

            return new WorkZoneRoadEventFeatureBuilder(value, _eventBuilder)
            {
                _geometry = _geometry,
                _boundingBox = _boundingBox
            };
        }

        public WorkZoneRoadEventFeatureBuilder WithEvent(Func<WorkZoneRoadEventBuilder, WorkZoneRoadEventBuilder> configure)
        {
            _eventBuilder = configure(_eventBuilder);
            return this;
        }

        public WorkZoneRoadEventFeatureBuilder WithGeometry(LineString value)
        {
            _geometry = value;
            _boundingBox = value.GetBoundaryBox();
            return this;
        }

        public WorkZoneRoadEventFeatureBuilder WithGeometry(MultiPoint value)
        {
            _geometry = value;
            _boundingBox = value.GetBoundaryBox();
            return this;
        }

        public RoadEventFeature Result()
        {
            return new RoadEventFeature()
            {
                Id = _featureId,
                Properties = _eventBuilder.Result(),
                Geometry = _geometry,
                BoundaryBox = _boundingBox?.ToList()
            };
        }
    }
}