using System.Net.Http.Headers;

namespace HttpRequestBuilder.Model
{
    /// <summary>
    /// Stores the data required to create the Http Client.
    /// </summary>
    internal class BuilderConfiguration
    {
        public UriBuilder Uri { get; set; } = new UriBuilder();
        public AuthenticationHeaderValue? Authentication { get; set; }
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    }
}