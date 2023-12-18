using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using Wzdx.Core;
using Wzdx.GeoJson.Geometries;

namespace Wzdx.v4.Devices
{
    /// <summary>
    /// Provides an abstract builder of a v4 FieldDeviceFeature (abstract) class
    /// </summary>
    public abstract class FieldDeviceFeatureBuilder<T, TProperties> : IBuilder<FieldDeviceFeature>
        where T : FieldDeviceFeatureBuilder<T, TProperties>
        where TProperties : IFieldDevice
    {
        private readonly IFactory<FieldDeviceFeature> _featureFactory;
        protected BuilderConfiguration<FieldDeviceFeature> FeatureConfiguration { get; }
            = new BuilderConfiguration<FieldDeviceFeature>();
        protected BuilderConfiguration<TProperties> PropertiesConfiguration { get; }
            = new BuilderConfiguration<TProperties>();
        protected BuilderConfiguration<FieldDeviceCoreDetails> CoreDetailConfiguration { get; }
            = new BuilderConfiguration<FieldDeviceCoreDetails>();

        protected FieldDeviceFeatureBuilder(IFactory<FieldDeviceFeature> featureFactory)
        {
            _featureFactory = featureFactory;
        }

        public T WithStatus(FieldDeviceStatus value)
        {
            CoreDetailConfiguration.Set(details => details.DeviceStatus, value);
            return Derived();
        }

        public T WithDescription(string value)
        {
            CoreDetailConfiguration.Set(details => details.Description, value);
            return Derived();
        }

        public T WithFirmwareVersion(string value)
        {
            CoreDetailConfiguration.Set(details => details.FirmwareVersion, value);
            return Derived();
        }

        public T WithVelocityKph(int? value)
        {
	        CoreDetailConfiguration.Set(details => details.VelocityKph, value);
	        return Derived();
        }

		public T WithHasAutomaticLocation(bool value)
        {
            CoreDetailConfiguration.Set(details => details.HasAutomaticLocation, value);
            return Derived();
        }

        public T WithName(string value)
        {
            CoreDetailConfiguration.Set(details => details.Name, value);
            return Derived();
        }

        public T WithMake(string value)
        {
            CoreDetailConfiguration.Set(details => details.Make, value);
            return Derived();
        }

        public T WithModel(string value)
        {
            CoreDetailConfiguration.Set(details => details.Model, value);
            return Derived();
        }

        public T WithUpdateDate(DateTimeOffset value)
        {
            CoreDetailConfiguration.Set(details => details.UpdateDate, value.ToUniversalTime());
            return Derived();
        }

        public T WithSerialNumber(string value)
        {
            CoreDetailConfiguration.Set(details => details.SerialNumber, value);
            return Derived();
        }
        
        public T WithRoadName(string value)
        {
            CoreDetailConfiguration.Combine(details => details.RoadNames, details =>
            {
                if (details.RoadNames == null) details.RoadNames = new List<string>();
                details.RoadNames.Add(value);
            });
            return Derived();
        }

        public T WithoutRoadName(string value)
        {
            CoreDetailConfiguration.Combine(details => details.RoadNames, details =>
            {
                if (details.RoadNames == null) return;
                details.RoadNames.Remove(value);
            });
            return Derived();
        }

        public T WithoutRoadNames()
        {
            CoreDetailConfiguration.Combine(details => details.RoadNames, details => details.RoadNames = null);
            return Derived();
        }

        public T WithMilepost(double value)
        {
            CoreDetailConfiguration.Set(details => details.Milepost, value);
            return Derived();
        }

        public T WithStatusMessage(string value)
        {
            CoreDetailConfiguration.Combine(details => details.StatusMessages, details =>
            {
                if (details.StatusMessages == null)
                    details.StatusMessages = new Collection<string>();

                details.StatusMessages.Add(value);
            });
            return Derived();
        }

        public T WithoutStatusMessage(string value)
        {
            CoreDetailConfiguration.Combine(details => details.StatusMessages, details =>
            {
                if (details.StatusMessages == null) return;

                details.StatusMessages.Remove(value);
            });
            return Derived();
        }

        public T WithNoStatusMessages()
        {
            CoreDetailConfiguration.Default(details => details.StatusMessages);
            return Derived();
        }

        public T WithGeometry(Point value)
        {
            if (value.BoundaryBox == null)
                value = Point.FromCoordinates(value.Coordinates);

            return WithGeometry(value, value.BoundaryBox);
        }

        private T WithGeometry(IGeometry geometry, IEnumerable<double> boundaryBox)
        {
            FeatureConfiguration.Set(feature => feature.Geometry, geometry);
            FeatureConfiguration.Set(feature => feature.BoundaryBox, boundaryBox);
            return Derived();
        }

        [Pure]
        public virtual FieldDeviceFeature Result()
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