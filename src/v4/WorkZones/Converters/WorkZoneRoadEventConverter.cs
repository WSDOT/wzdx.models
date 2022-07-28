using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;

namespace Wzdx.v4.WorkZones.Converters
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "Suppressing unused type loaded by reflection")]
    internal sealed class WorkZoneRoadEventConverter : IRoadEventConverter
    {
        public IRoadEvent Read(JObject value)
        {
            return value.ToObject<WorkZoneRoadEvent>();
        }

        public bool CanConvert(RoadEventCoreDetails details)
        {
            return details.EventType == EventType.WorkZone;
        }
    }
}