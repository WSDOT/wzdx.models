using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.GeoJson.Geometries;

namespace Wsdot.Wzdx.v4.Devices
{
    public sealed class CameraFeatureBuilder : FieldDeviceFeatureBuilder<CameraFeatureBuilder, Camera>
    {
        public CameraFeatureBuilder(string sourceId, string featureId, string roadName) :
            this(new List<Action<FieldDeviceFeature>>(), (feature, properties) =>
            {
                var geometry = Point.FromCoordinates(Position.From(0, 0));
                feature.Id = featureId;
                feature.Geometry = geometry;
                feature.BoundaryBox = geometry.BoundaryBox.ToList().AsReadOnly();

                properties.CoreDetails.DataSourceId = sourceId;
                properties.CoreDetails.RoadNames.Add(roadName);
                properties.CoreDetails.DeviceStatus = FieldDeviceStatus.Unknown;
            })
        {
            // ignore
        }

        private CameraFeatureBuilder(IEnumerable<Action<FieldDeviceFeature>> configuration,
            Action<FieldDeviceFeature, Camera> step)
            : base(configuration, step)
        {
            // ignore
        }

        public CameraFeatureBuilder WithImage(Uri url, DateTimeOffset timestamp)
        {
            return CreateWith((feature, camera) =>
            {
                camera.ImageUrl = url;
                camera.ImageTimestamp = timestamp.UtcDateTime;
            });
        }

        protected override CameraFeatureBuilder CreateWith(Action<FieldDeviceFeature, Camera> step)
        {
            return new CameraFeatureBuilder(Configuration, step);
        }

        protected override Func<Camera> ResultProperties { get; } = () => new Camera();
        
    }
}