using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.GeoJson.Geometries;

namespace Wsdot.Wzdx.v4.Devices
{
    public abstract class FieldDeviceFeatureBuilder<TBuilder, TProperties> : Builder<FieldDeviceFeature>
        where TBuilder : Builder<FieldDeviceFeature>
        where TProperties : IFieldDevice
    {
        protected FieldDeviceFeatureBuilder(IEnumerable<Action<FieldDeviceFeature>> configuration, Action<FieldDeviceFeature, TProperties> step) :
            base(configuration, feature => step(feature, (TProperties)feature.Properties))
        {

        }

        public TBuilder WithStatus(FieldDeviceStatus value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.DeviceStatus = value);
        }

        public TBuilder WithDescription(string value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.Description = value);
        }

        public TBuilder WithFirmwareVersion(string value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.FirmwareVersion = value);
        }

        public TBuilder WithHasAutomaticLocation(bool value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.HasAutomaticLocation = value);
        }
        public TBuilder WithName(string value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.Name = value);
        }

        public TBuilder WithMake(string value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.Make = value);
        }
        public TBuilder WithModel(string value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.Model = value);
        }

        public TBuilder WithUpdateDate(DateTimeOffset value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.UpdateDate = value.ToUniversalTime());
        }

        public TBuilder WithSerialNumber(string value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.SerialNumber = value);
        }

        public TBuilder WithAdditionalRoadName(string value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.RoadNames.Add(value));
        }

        public TBuilder WithMilepost(double value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.Milepost = value);
        }

        public TBuilder WithStatusMessage(string value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.StatusMessages.Add(value));
        }

        public TBuilder WithoutStatusMessage(string value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.StatusMessages.Remove(value));
        }

        public TBuilder WithNoStatusMessages()
        {
            return CreateWith((_, properties) => properties.CoreDetails.StatusMessages.Clear());
        }

        public TBuilder WithGeometry(Point value)
        {
            return CreateWith((feature, _) =>
            {
                feature.Geometry = value;
                feature.BoundaryBox = value.BoundaryBox.ToList().AsReadOnly();
            });
        }

        protected abstract TBuilder CreateWith(Action<FieldDeviceFeature, TProperties> step);

        protected abstract Func<TProperties> ResultProperties { get; }

        protected override Func<FieldDeviceFeature> ResultFactory
        {
            get
            {
                return () => new FieldDeviceFeature()
                {
                    Properties = ResultProperties()
                };
            }
        }
    }
}