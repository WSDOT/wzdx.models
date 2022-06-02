using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.GeoJson.Geometries;
using Wsdot.Wzdx.v4.Devices;

namespace Wsdot.Wzdx.v4.Builders
{
    public class DynamicMessageSignFeatureBuilder : FieldDeviceFeatureBuilder<DynamicMessageSignFeatureBuilder, DynamicMessageSign>
    {
        public DynamicMessageSignFeatureBuilder(string sourceId, string featureId, string roadName) :
            this(new List<Action<FieldDeviceFeature>>(), (feature, properties) =>
            {
                var geometry = Point.FromCoordinates(Position.From(0, 0));
                feature.Id = featureId;
                feature.Geometry = geometry;
                feature.BoundaryBox = geometry.BoundaryBox.ToList().AsReadOnly();

                properties.CoreDetails.DataSourceId = sourceId;
                properties.CoreDetails.RoadNames.Add(roadName);
                properties.CoreDetails.DeviceStatus = FieldDeviceStatus.Unknown;
                properties.MessageMultiString = string.Empty;
            })
        {
            // ignore
        }

        private DynamicMessageSignFeatureBuilder(IEnumerable<Action<FieldDeviceFeature>> configuration, Action<FieldDeviceFeature, DynamicMessageSign> step) : 
            base(configuration, step)
        {
            // ignore
        }

        public DynamicMessageSignFeatureBuilder WithMessage(string value)
        {
            return CreateWith((feature, sign) => sign.MessageMultiString = value);
        }

        protected override DynamicMessageSignFeatureBuilder CreateWith(Action<FieldDeviceFeature, DynamicMessageSign> step)
        {
            return new DynamicMessageSignFeatureBuilder(Configuration, step);
        }

        protected override Func<DynamicMessageSign> ResultProperties { get; } =
            () => new DynamicMessageSign();
    }
}