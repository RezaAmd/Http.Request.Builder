using System.Reflection;
using System.Text.Json.Serialization;

namespace Http.Request.Builder.Extensions
{
    internal static class AttributeExtensions
    {
        public static string GetJsonPropertyValue(this PropertyInfo property)
        {
            var attribute = property
                .GetCustomAttribute<JsonPropertyNameAttribute>();
            if (attribute is null)
                return property.Name;
            return attribute.Name;
        }
    }
}