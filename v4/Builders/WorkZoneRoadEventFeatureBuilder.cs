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
        private readonly WorkZoneRoadEventBuilder _eventBuilder;

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

        public WorkZoneRoadEventFeatureBuilder WithGeometry(IGeometry value)
        {
            _geometry = value;
            _boundingBox = value.GetBoundaryBox();
            return this;
        }
    }

    internal static class GeometryBoundingBoxExtensions
    {
        public static IEnumerable<double> GetBoundaryBox(this IGeometry geometry)
        {
            if (geometry == null) return Enumerable.Empty<double>();
            switch (geometry.Type)
            {
                case GeometryType.None:
                    return new double[4];
                case GeometryType.Point:
                    return ((Point)geometry).BoundaryBox;
                case GeometryType.MultiPoint:
                    return ((MultiPoint)geometry).BoundaryBox;
                case GeometryType.LineString:
                    return ((LineString)geometry).BoundaryBox;
                default:
                    throw new ArgumentOutOfRangeException(nameof(geometry.Type));
            }
        }
    }
}