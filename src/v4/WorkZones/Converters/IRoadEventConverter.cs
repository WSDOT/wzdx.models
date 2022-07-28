using Newtonsoft.Json.Linq;

namespace Wzdx.v4.WorkZones.Converters
{
    internal interface IRoadEventConverter
    {
        IRoadEvent Read(JObject value);
        bool CanConvert(RoadEventCoreDetails details);
    }
}