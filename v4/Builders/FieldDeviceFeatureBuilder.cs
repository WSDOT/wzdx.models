using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.v4.Devices;

namespace Wsdot.Wzdx.v4.Builders
{
    public abstract class FieldDeviceFeatureBuilder : IBuilder<FieldDeviceFeature>
    {

        public abstract FieldDeviceFeature Result();
    }
}