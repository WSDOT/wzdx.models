using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Wzdx.Core;
using Wzdx.GeoJson.Geometries;

namespace Wzdx.v4.Devices
{
    /// <summary>
    /// Provides a builder for a v4 FieldDeviceFeature (LocationMarker) class
    /// </summary>
    public class LocationMarkerFeatureBuilder :
        FieldDeviceFeatureBuilder<LocationMarkerFeatureBuilder, LocationMarker>
    {

        public LocationMarkerFeatureBuilder(string sourceId, string featureId, string roadName) :
            base(new DelegatingFactory<FieldDeviceFeature>(() => new FieldDeviceFeature() { Properties = new LocationMarker() }))
        {
            FeatureConfiguration.Set(feature => feature.Id, featureId);
            CoreDetailConfiguration.Set(details => details.DataSourceId, sourceId);
            WithGeometry(Point.FromCoordinates(Position.From(0, 0)));
            WithRoadName(roadName);
            WithStatus(FieldDeviceStatus.Unknown);
            WithMarkedLocation(MarkedLocationType.Flagger, builder => builder);
        }

        /// <summary>
        /// Removes all marked locations
        /// </summary>
        public LocationMarkerFeatureBuilder WithoutMarkedLocations()
        {
            PropertiesConfiguration.Default(properties => properties.MarkedLocations, new Collection<MarkedLocation>());
            return Derived();
        }

        /// <summary>
        /// Sets a single marked location for the feature
        /// </summary>
        /// <param name="type">Marked location type</param>
        public LocationMarkerFeatureBuilder WithMarkedLocation(MarkedLocationType type)
        {
            return WithMarkedLocation(type, builder => builder);
        }

        /// <summary>
        /// Sets a single marked location for the feature
        /// </summary>
        /// <param name="type">Marked location type</param>
        /// <param name="config">Marked location builder factory</param>
        public LocationMarkerFeatureBuilder WithMarkedLocation(MarkedLocationType type, Func<MarkedLocationBuilder, IBuilder<MarkedLocation>> config)
        {
            PropertiesConfiguration.Default(properties => properties.MarkedLocations, new Collection<MarkedLocation>());
            return WithMarkedLocations(type, new[] { config });
        }

        /// <summary>
        /// Sets additional marked locations for the feature
        /// </summary>
        /// <param name="type">Default marked location type for locations</param>
        /// <param name="configs">Marked location builder factories</param>
        public LocationMarkerFeatureBuilder WithMarkedLocations(MarkedLocationType type, IEnumerable<Func<MarkedLocationBuilder, IBuilder<MarkedLocation>>> configs)
        {
            var values = configs.Select(config => config(new MarkedLocationBuilder(type)).Result()).ToList().AsReadOnly();

            PropertiesConfiguration.Combine(properties => properties.MarkedLocations, properties =>
            {
                foreach (var value in values)
                {
                    properties.MarkedLocations.Add(value);
                }
            });

            return Derived();
        }
    }
}