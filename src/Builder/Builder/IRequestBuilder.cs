using Http.Request.Builder.Request;

namespace Http.Request.Builder.Builder
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