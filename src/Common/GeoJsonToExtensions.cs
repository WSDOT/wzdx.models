using Newtonsoft.Json;
using Wzdx.GeoJson;

namespace Wzdx.Common
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
