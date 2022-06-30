using Newtonsoft.Json.Linq;

namespace Wsdot.Wzdx.v4.Devices.Converters
{
    internal interface IFieldDeviceConverter
    {
        IFieldDevice Read(JObject value);
        bool CanConvert(FieldDeviceCoreDetails details);
    }
}