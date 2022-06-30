using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;

namespace Wsdot.Wzdx.v4.WorkZones.Converters
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "Suppressing unused type loaded by reflection")]
    internal class DetourRoadEventConverter : IRoadEventConverter
    {
        public IRoadEvent Read(JObject value)
        {
            return value.ToObject<DetourRoadEvent>();
        }

        public bool CanConvert(RoadEventCoreDetails details)
        {
            return details.EventType == EventType.Detour;
        }
    }
}