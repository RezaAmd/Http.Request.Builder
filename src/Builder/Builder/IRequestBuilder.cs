using HttpRequestBuilder.Request;

namespace HttpRequestBuilder.Builder
{
    public interface IRequestBuilder
    {
        /// <summary>
        /// Build a new http request.
        /// </summary>
        /// <returns></returns>
        IHttpRequest Build();
    }
}