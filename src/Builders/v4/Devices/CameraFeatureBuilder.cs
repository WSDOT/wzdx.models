using System;
using Wzdx.Core;
using Wzdx.GeoJson.Geometries;

namespace Wzdx.v4.Devices
{
    /// <summary>
    /// Provides a builder for a v4 FieldDeviceFeature (Camera) class
    /// </summary>
    public class CameraFeatureBuilder : FieldDeviceFeatureBuilder<CameraFeatureBuilder, Camera>
    {
        public CameraFeatureBuilder(string sourceId, string featureId, string roadName) :
            base(new DelegatingFactory<FieldDeviceFeature>(() => new FieldDeviceFeature() { Properties = new Camera() }))
        {
            FeatureConfiguration.Set(feature => feature.Id, featureId);
            CoreDetailConfiguration.Set(details => details.DataSourceId, sourceId);
            WithGeometry(Point.FromCoordinates(Position.From(0, 0)));
            WithRoadName(roadName);
            WithStatus(FieldDeviceStatus.Unknown);
        }

        public CameraFeatureBuilder WithImage(Uri url, DateTimeOffset timestamp)
        {
            PropertiesConfiguration.Set(properties => properties.ImageUrl, url);
            PropertiesConfiguration.Set(properties => properties.ImageTimestamp, timestamp.ToLocalTime());
            return Derived();
        }
    }
}