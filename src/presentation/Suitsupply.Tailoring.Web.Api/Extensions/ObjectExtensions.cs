using Newtonsoft.Json;

namespace Suitsupply.Tailoring.Web.Api.Extensions
{
    public static class ObjectExtensions 
    {
        public static string ToJson(this object target)
        {
            return JsonConvert.SerializeObject(target, Formatting.Indented);
        }

        public static T FromJson<T>(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(source);
        }
    }
}