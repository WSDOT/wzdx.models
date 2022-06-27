using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.GeoJson.Geometries;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.RoadEvents
{
    /// <summary>
    /// Provides an abstract immutable builder of a v4 RoadEventFeature (abstract) class
    /// </summary>
    public abstract class RoadEventFeatureBuilder<TBuilder, TProperties> : Builder<RoadEventFeature>
        where TBuilder : Builder<RoadEventFeature>
        where TProperties : IRoadEvent
    {

        protected RoadEventFeatureBuilder(IEnumerable<Action<RoadEventFeature>> configuration, Action<RoadEventFeature, TProperties> step) :
            base(configuration, feature => step(feature, (TProperties)feature.Properties))
        {

        }

        // ReSharper disable once UnusedMember.Global
        [Pure]
        public TBuilder WithRoadName(string value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.RoadNames.Add(value));
        }

        // ReSharper disable once UnusedMember.Global
        [Pure]
        public TBuilder WithDirection(Direction value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.Direction = value);
        }

        // ReSharper disable once UnusedMember.Global
        [Pure]
        public TBuilder WithDescription(string value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.Description = value);
        }

        // ReSharper disable once UnusedMember.Global
        [Pure]
        public TBuilder WithRelationship(Relationship value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.Relationship = value);
        }

        // ReSharper disable once UnusedMember.Global
        [Pure]
        public TBuilder WithCreated(DateTimeOffset value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.CreationDate = value);
        }

        // ReSharper disable once UnusedMember.Global
        [Pure]
        public TBuilder WithUpdated(DateTimeOffset value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.UpdateDate = value);
        }

        [Pure]
        public TBuilder WithGeometry(LineString value)
        {
            if (value.BoundaryBox == null)
                value.BoundaryBox = value.Coordinates.AsBoundaryBox();

            return CreateWith((feature, _) =>
            {
                feature.Geometry = value;
                feature.BoundaryBox = value.BoundaryBox.ToList().AsReadOnly();
            });
        }

        [Pure]
        public TBuilder WithGeometry(MultiPoint value)
        {
            if (value.BoundaryBox == null)
                value.BoundaryBox = value.Coordinates.AsBoundaryBox();

            return CreateWith((feature, _) =>
            {
                feature.Geometry = value;
                feature.BoundaryBox = value.BoundaryBox.ToList().AsReadOnly();
            });
        }

        protected abstract TBuilder CreateWith(Action<RoadEventFeature, TProperties> step);
        
        protected abstract Func<TProperties> ResultProperties { get; }
        
        protected override Func<RoadEventFeature> ResultFactory
        {
            get
            {
                return () => new RoadEventFeature()
                {
                    Properties = ResultProperties()
                };
            }
        }
    }
}