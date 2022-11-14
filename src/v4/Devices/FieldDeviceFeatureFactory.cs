using System;
using Wzdx.GeoJson.Geometries;

namespace Wzdx.v4.Devices
{
    public class FieldDeviceFeatureFactory
    {
        public static FieldDeviceFeature CreateFeature(string source, string id, FieldDeviceType deviceType)
        {
            var details = new FieldDeviceCoreDetails()
            {
                DataSourceId = source,
                DeviceType = deviceType
            };

            IFieldDevice properties;
            switch (deviceType)
            {
                case FieldDeviceType.ArrowBoard:
                    properties = new ArrowBoard()
                    {
                        CoreDetails = details
                    };
                    break;
                case FieldDeviceType.Camera:
                    properties = new Camera()
                    {
                        CoreDetails = details
                    };

                    break;
                case FieldDeviceType.DynamicMessageSign:
                    properties = new DynamicMessageSign()
                    {
                        CoreDetails = details
                    };

                    break;
                case FieldDeviceType.FlashingBeacon:
                    properties = new FlashingBeacon()
                    {
                        CoreDetails = details
                    };

                    break;
                case FieldDeviceType.HybridSign:
                    properties = new HybridSign()
                    {
                        CoreDetails = details
                    };

                    break;
                case FieldDeviceType.LocationMarker:
                    properties = new LocationMarker()
                    {
                        CoreDetails = details
                    };

                    break;
                case FieldDeviceType.TrafficSensor:
                    properties = new TrafficSensor()
                    {
                        CoreDetails = details
                    };

                    break;
                case FieldDeviceType.TrafficSignal:
                    properties = new TrafficSignal()
                    {
                        CoreDetails = details
                    };

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(deviceType), deviceType, null);
            }

            return new FieldDeviceFeature()
            {
                Id = id,
                Properties = properties,
                Geometry = GeometryFactory.CreateNull()
            };
        }
    }
}
