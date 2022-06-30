using Newtonsoft.Json;
using Wsdot.Wzdx.GeoJson;

namespace Wsdot.Wzdx
{
    public static class GeoJsonToExtensions
    {
        public static string ToJson<T>(this FeatureCollection<T> source) 
            where T : IFeature
        {
            return JsonConvert.SerializeObject(source);
        }

        public static string ToJson<T>(this IFeature source)
            where T : IFeature
        {
            return JsonConvert.SerializeObject(source);
        }
    }
}
