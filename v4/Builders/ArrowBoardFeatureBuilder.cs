using Wsdot.Wzdx.GeoJson.Geometries;
using Wsdot.Wzdx.v4.Devices;

namespace Wsdot.Wzdx.v4.Builders
{
    public class ArrowBoardFeatureBuilder : FieldDeviceFeatureBuilder
    {
        private readonly string _sourceId;
        private readonly string _featureId;
        private readonly string _roadName;
        private FieldDeviceStatus _status;

        public ArrowBoardFeatureBuilder(string sourceId, string featureId, string roadName)
        {
            _sourceId = sourceId;
            _featureId = featureId;
            _roadName = roadName;
            _status = FieldDeviceStatus.Unknown;
        }

        public ArrowBoardFeatureBuilder WithStatus(FieldDeviceStatus value)
        {
            _status = value;
            return this;
        }

        public override FieldDeviceFeature Result()
        {
            return new FieldDeviceFeature()
            {
                Id = _featureId,
                Properties = new ArrowBoard()
                {
                    CoreDetails = new FieldDeviceCoreDetails()
                    {
                        DataSourceId = _sourceId,
                        DeviceType = FieldDeviceType.ArrowBoard,
                        DeviceStatus = _status,
                        RoadNames = new []
                        {
                            _roadName
                        }
                    }
                },
                Geometry = new Point()
                {
                    Coordinates = new Position(10, 1)
                }
            };
        }

    }
}