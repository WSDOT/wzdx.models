using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Wsdot.Wzdx.GeoJson.Geometries;

namespace Wsdot.Wzdx.v4.Devices
{
    /// <summary>
    /// Provides an immutable builder of a v4 FieldDeviceFeature (HybridSign) class
    /// </summary>
    public class HybridSignFeatureBuilder :
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
        
        [Pure]
        public HybridSignFeatureBuilder WithFunction(HybridSignDynamicMessageFunction value)
        {
            return CreateWith((_, sign) => sign.DynamicMessageFunction = value);
        }

        [Pure]
        public HybridSignFeatureBuilder WithDynamicMessage(string value)
        {
            return CreateWith((_, sign) => sign.DynamicMessageText = value);
        }

        [Pure]
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