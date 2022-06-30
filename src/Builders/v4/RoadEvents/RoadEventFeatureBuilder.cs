using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.GeoJson.Geometries;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.RoadEvents
{
    /// <summary>
    /// Provides an abstract builder of a v4 RoadEventFeature (abstract) class
    /// </summary>
    public abstract class RoadEventFeatureBuilder<T, TProperties> : IBuilder<RoadEventFeature>
        where T : RoadEventFeatureBuilder<T, TProperties>
        where TProperties : IRoadEvent
    {
        private readonly IFactory<RoadEventFeature> _featureFactory;
        protected BuilderConfiguration<RoadEventFeature> FeatureConfiguration { get; }
            = new BuilderConfiguration<RoadEventFeature>();
        protected BuilderConfiguration<TProperties> PropertiesConfiguration { get; }
            = new BuilderConfiguration<TProperties>();
        protected BuilderConfiguration<RoadEventCoreDetails> CoreDetailConfiguration { get; }
            = new BuilderConfiguration<RoadEventCoreDetails>();

        protected RoadEventFeatureBuilder(IFactory<RoadEventFeature> featureFactory)
        {
            _featureFactory = featureFactory;
        }
        
        public T WithRoadName(string value)
        {
            CoreDetailConfiguration.Combine(details => details.RoadNames, details =>
            {
                details.RoadNames.Add(value);
            });

            return Derived();
        }
        
        public T WithDirection(Direction value)
        {
            CoreDetailConfiguration.Set(details => details.Direction, value);
            return Derived();
        }
        
        public T WithDescription(string value)
        {
            CoreDetailConfiguration.Set(details => details.Description, value);
            return Derived();
        }

        public T WithRelationship(Relationship value)
        {
            CoreDetailConfiguration.Set(details => details.Relationship, value);
            return Derived();
        }

        public T WithCreated(DateTimeOffset value)
        {
            CoreDetailConfiguration.Set(details => details.CreationDate, value.ToUniversalTime());
            return Derived();
        }

        public T WithUpdated(DateTimeOffset value)
        {
            CoreDetailConfiguration.Set(details => details.UpdateDate, value.ToUniversalTime());
            return Derived();
        }

        public T WithGeometry(LineString value)
        {
            if (value.BoundaryBox == null)
                value.BoundaryBox = value.Coordinates.AsBoundaryBox();

            return WithGeometry(value, value.BoundaryBox);
        }

        public T WithGeometry(MultiPoint value)
        {
            if (value.BoundaryBox == null)
                value.BoundaryBox = value.Coordinates.AsBoundaryBox();

            return WithGeometry(value, value.BoundaryBox);
        }

        private T WithGeometry(IGeometry geometry, IEnumerable<double> boundaryBox)
        {
            FeatureConfiguration.Set(feature => feature.Geometry, geometry);
            FeatureConfiguration.Set(feature => feature.BoundaryBox, boundaryBox);
            return Derived();
        }

        [Pure]
        public virtual RoadEventFeature Result()
        {
            var result = _featureFactory.Create();
            FeatureConfiguration.ApplyTo(result);
            PropertiesConfiguration.ApplyTo((TProperties)result.Properties);
            CoreDetailConfiguration.ApplyTo(result.Properties.CoreDetails);
            return result;
        }

        protected T Derived() => (T)this;
    }
}