using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.GeoJson.Geometries;

namespace Wsdot.Wzdx.v4.Devices
{
    /// <summary>
    /// Provides an abstract immutable builder of a v4 FieldDeviceFeature (abstract) class
    /// </summary>
    public abstract class FieldDeviceFeatureBuilder<TBuilder, TProperties> : Builder<FieldDeviceFeature>
        where TBuilder : Builder<FieldDeviceFeature>
        where TProperties : IFieldDevice
    {
        protected FieldDeviceFeatureBuilder(IEnumerable<Action<FieldDeviceFeature>> configuration, Action<FieldDeviceFeature, TProperties> step) :
            base(configuration, feature => step(feature, (TProperties)feature.Properties))
        {

        }

        [Pure]
        public TBuilder WithStatus(FieldDeviceStatus value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.DeviceStatus = value);
        }

        [Pure]
        public TBuilder WithDescription(string value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.Description = value);
        }

        [Pure]
        public TBuilder WithFirmwareVersion(string value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.FirmwareVersion = value);
        }

        [Pure]
        public TBuilder WithHasAutomaticLocation(bool value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.HasAutomaticLocation = value);
        }

        [Pure]
        public TBuilder WithName(string value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.Name = value);
        }

        [Pure]
        public TBuilder WithMake(string value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.Make = value);
        }

        [Pure]
        public TBuilder WithModel(string value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.Model = value);
        }

        [Pure]
        public TBuilder WithUpdateDate(DateTimeOffset value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.UpdateDate = value.ToUniversalTime());
        }

        [Pure]
        public TBuilder WithSerialNumber(string value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.SerialNumber = value);
        }

        [Pure]
        public TBuilder WithAdditionalRoadName(string value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.RoadNames.Add(value));
        }

        [Pure]
        public TBuilder WithMilepost(double value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.Milepost = value);
        }

        [Pure]
        public TBuilder WithStatusMessage(string value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.StatusMessages.Add(value));
        }

        [Pure]
        public TBuilder WithoutStatusMessage(string value)
        {
            return CreateWith((_, properties) => properties.CoreDetails.StatusMessages.Remove(value));
        }

        [Pure]
        public TBuilder WithNoStatusMessages()
        {
            return CreateWith((_, properties) => properties.CoreDetails.StatusMessages.Clear());
        }

        [Pure]
        public TBuilder WithGeometry(Point value)
        {
            if (value.BoundaryBox == null)
                value = Point.FromCoordinates(value.Coordinates);

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