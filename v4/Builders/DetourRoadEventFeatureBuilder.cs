using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.GeoJson.Geometries;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.Builders
{
    public class DetourRoadEventFeatureBuilder : IBuilder<RoadEventFeature>
    {
        private readonly string _featureId;
        private IGeometry _geometry;
        private IEnumerable<double> _boundingBox;
        private readonly DetourRoadEventBuilder _eventBuilder;

        public DetourRoadEventFeatureBuilder(string sourceId, string featureId, string roadName, Direction direction)
        {
            _featureId = featureId;
            _eventBuilder = new DetourRoadEventBuilder(sourceId, roadName, direction);
            _geometry = MultiPoint.FromCoordinates(Enumerable.Empty<Position>());
        }

        public DetourRoadEventFeatureBuilder WithEvent(Action<DetourRoadEventBuilder> configure)
        {
            configure(_eventBuilder);
            return this;
        }

        public DetourRoadEventFeatureBuilder WithGeometry(LineString value)
        {
            this._geometry = value;
            this._boundingBox = value.BoundaryBox;
            return this;
        }

        public DetourRoadEventFeatureBuilder WithGeometry(MultiPoint value)
        {
            this._geometry = value;
            this._boundingBox = value.BoundaryBox;
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