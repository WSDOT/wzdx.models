using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.GeoJson;
using Wsdot.Wzdx.GeoJson.Geometries;
using Wsdot.Wzdx.v4.Feeds;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.RoadEvents
{
    public delegate IFeatureCollectionBuilder<TFeatureBuilderFactory, TFeatureBuilder, TFeature> FeatureCollectionBuilderSetup<TFeatureBuilderFactory, TFeatureBuilder, TFeature>
        (IFeatureCollectionBuilder<TFeatureBuilderFactory, TFeatureBuilder, TFeature> builder)
        where TFeatureBuilderFactory : IFeatureBuilderFactory
        where TFeatureBuilder : IBuilder<TFeature>
        where TFeature : IFeature
    ;

    public interface IFeatureCollectionBuilder<out TFeatureBuilderFactory, in TFeatureBuilder, out TFeature> : IBuilder<IEnumerable<TFeature>>
        where TFeatureBuilderFactory : IFeatureBuilderFactory
        where TFeatureBuilder : IBuilder<TFeature>
        where TFeature : IFeature
    {
        IFeatureCollectionBuilder<TFeatureBuilderFactory, TFeatureBuilder, TFeature> WithFeature(string featureId, Func<TFeatureBuilderFactory, TFeatureBuilder> setup);
    }

    public delegate Func<TFeatureBuilderFactory, TFeatureBuilder, TFeature> FeatureBuilderFactory<in TFeatureBuilderFactory, in TFeatureBuilder, out TFeature>()
        where TFeatureBuilderFactory : IFeatureBuilderFactory
        where TFeatureBuilder : IBuilder<TFeature>
        where TFeature : IFeature
    ;

    public abstract class FeatureCollectionBuilder<TFeatureBuilderFactory, TFeatureBuilder, TFeature> :
        IFeatureCollectionBuilder<TFeatureBuilderFactory, TFeatureBuilder, TFeature>
        where TFeatureBuilderFactory : class, IFeatureBuilderFactory
        where TFeatureBuilder : IBuilder<TFeature>
        where TFeature : IFeature
    {
        private readonly ICollection<TFeatureBuilder> _builders;
        
        private readonly string _sourceId;

        public FeatureCollectionBuilder(string sourceId)
        {
            _sourceId = sourceId;
            _builders = new List<TFeatureBuilder>();
        }

        public virtual IFeatureCollectionBuilder<TFeatureBuilderFactory, TFeatureBuilder, TFeature> WithFeature(string featureId, Func<TFeatureBuilderFactory, TFeatureBuilder> setup)
        {
            _builders.Add(setup(new FeatureBuilderFactory(_sourceId, featureId) as TFeatureBuilderFactory));
            return this;
        }

        public IEnumerable<TFeature> Result()
        {
            return _builders.Select(builder => builder.Result());
        }
    }

    /// <summary>
    /// Provides a builder for a v4 RoadEventFeature (Restriction) class
    /// </summary>
    public sealed class RoadRestrictionFeatureBuilder : RoadEventFeatureBuilder<RoadRestrictionFeatureBuilder, RestrictionRoadEvent>
    {
        public RoadRestrictionFeatureBuilder(string sourceId, string featureId, string roadName, Direction direction) :
            base(new DelegatingFactory<RoadEventFeature>(() => new RoadEventFeature() { Properties = new RestrictionRoadEvent() }))
        {
            FeatureConfiguration.Set(feature => feature.Id, featureId);
            CoreDetailConfiguration.Set(details => details.DataSourceId, sourceId);
            WithGeometry(MultiPoint.FromCoordinates(Enumerable.Empty<Position>()));
            WithRoadName(roadName);
            WithDirection(direction);
        }

        public RoadRestrictionFeatureBuilder WithLane(LaneType type, LaneStatus status, int order, Func<LaneBuilder, LaneBuilder> configure)
        {
            var builder = configure(new LaneBuilder(type, status, order));
            var lane = builder.Result();
            PropertiesConfiguration.Combine(properties => properties.Lanes, properties => properties.Lanes.Add(lane));
            return Derived();
        }

        public RoadRestrictionFeatureBuilder WithRestriction(RestrictionType type, UnitOfMeasurement unit, Func<RestrictionBuilder, RestrictionBuilder> configure)
        {
            var builder = configure(new RestrictionBuilder(type, unit));
            var restriction = builder.Result();
            PropertiesConfiguration.Combine(properties => properties.Restrictions, properties => properties.Restrictions.Add(restriction));
            return Derived();
        }
    }
}