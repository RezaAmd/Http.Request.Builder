using Newtonsoft.Json;
using System.Reflection;

namespace Http.Request.Builder.Extensions
{
    internal static class AttributeExtensions
    {
        public static string GetJsonPropertyValue(this PropertyInfo property)
        {
            return property
                .GetCustomAttribute<JsonPropertyAttribute>()
                .PropertyName;
        }
    }
}