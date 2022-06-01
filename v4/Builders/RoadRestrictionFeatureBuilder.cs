using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.GeoJson.Geometries;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.Builders
{

    public class RoadRestrictionFeatureBuilderFactory : IFactory<IBuilder<RoadEventFeature>>
    {
        private readonly string _sourceId;
        private readonly string _featureId;
        private readonly string _roadName;
        private readonly Direction _direction;

        public RoadRestrictionFeatureBuilderFactory(string sourceId, string featureId, string roadName, Direction direction)
        {
            _sourceId = sourceId;
            _featureId = featureId;
            _roadName = roadName;
            _direction = direction;
        }

        public IBuilder<RoadEventFeature> Create()
        {
            return new RoadRestrictionFeatureBuilder(_sourceId, _featureId, _roadName, _direction);
        }
    }

    public class RoadRestrictionFeatureBuilder : IBuilder<RoadEventFeature>
    {
        private readonly string _featureId;
        private IGeometry _geometry;
        private IEnumerable<double> _boundingBox;
        private readonly RoadRestrictionEventBuilder _eventBuilder;

        public RoadRestrictionFeatureBuilder(string sourceId, string featureId, string roadName, Direction direction)
        {
            _featureId = featureId;
            _eventBuilder = new RoadRestrictionEventBuilder(sourceId, roadName, direction);
            _geometry = MultiPoint.FromCoordinates(Enumerable.Empty<Position>());
        }

        public RoadRestrictionFeatureBuilder WithEvent(Action<RoadRestrictionEventBuilder> configure)
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

        public RoadRestrictionFeatureBuilder WithGeometry(MultiPoint value)
        {
            this._geometry = value;
            this._boundingBox = value.BoundaryBox;
            return this;
        }
    }
}