using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.GeoJson.Geometries;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.Builders
{
    public class WorkZoneRoadEventFeatureBuilder : IBuilder<RoadEventFeature>
    {
        private readonly string _featureId;
        private IGeometry _geometry;
        private IEnumerable<double> _boundingBox;
        private readonly WorkZoneRoadEventBuilder _eventBuilder;

        public WorkZoneRoadEventFeatureBuilder(string sourceId, string featureId, string roadName, Direction direction)
        {
            _featureId = featureId;
            _eventBuilder = new WorkZoneRoadEventBuilder(sourceId, roadName, direction);
            _geometry = MultiPoint.FromCoordinates(Enumerable.Empty<Position>());
        }

        public WorkZoneRoadEventFeatureBuilder WithEvent(Action<WorkZoneRoadEventBuilder> configure)
        {
            configure(_eventBuilder);
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

        public WorkZoneRoadEventFeatureBuilder WithGeometry(MultiPoint value)
        {
            this._geometry = value;
            this._boundingBox = value.BoundaryBox;
            return this;
        }
    }
}