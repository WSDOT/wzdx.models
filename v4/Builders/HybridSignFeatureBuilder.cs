using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.GeoJson.Geometries;
using Wsdot.Wzdx.v4.Devices;

namespace Wsdot.Wzdx.v4.Builders
{
    public sealed class HybridSignFeatureBuilder :
        FieldDeviceFeatureBuilder<HybridSignFeatureBuilder, HybridSign>
    {
        public HybridSignFeatureBuilder(string sourceId, string featureId, string roadName) :
            this(new List<Action<FieldDeviceFeature>>(), (feature, sign) =>
            {
                var geometry = Point.FromCoordinates(Position.From(0, 0));
                feature.Id = featureId;
                feature.Geometry = geometry;
                feature.BoundaryBox = geometry.BoundaryBox.ToList().AsReadOnly();

                sign.CoreDetails.DataSourceId = sourceId;
                sign.CoreDetails.RoadNames.Add(roadName);
                sign.DynamicMessageFunction = HybridSignDynamicMessageFunction.Other;
            })
        {
            // ignore
        }

        private HybridSignFeatureBuilder(IEnumerable<Action<FieldDeviceFeature>> configuration, Action<FieldDeviceFeature, HybridSign> step) :
            base(configuration, step)
        {
            // ignore
        }

        public HybridSignFeatureBuilder WithFunction(HybridSignDynamicMessageFunction value)
        {
            return CreateWith((_, sign) => sign.DynamicMessageFunction = value);
        }
        
        public HybridSignFeatureBuilder WithDynamicMessage(string value)
        {
            return CreateWith((_, sign) => sign.DynamicMessageText = value);
        }

        public HybridSignFeatureBuilder WithStaticMessage(string value)
        {
            return CreateWith((_, sign) => sign.StaticSignText = value);
        }

        protected override HybridSignFeatureBuilder CreateWith(Action<FieldDeviceFeature, HybridSign> step)
        {
            return new HybridSignFeatureBuilder(Configuration, step);
        }

        protected override Func<HybridSign> ResultProperties { get; } = () => new HybridSign();

    }
}