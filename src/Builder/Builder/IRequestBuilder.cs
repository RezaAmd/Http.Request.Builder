using Http.Request.Builder.Request;

namespace Http.Request.Builder.Builder
{
    public interface IRequestBuilder
    {

        /// <summary>
        /// If the response is failed, it will try again. (Retry Pattern)
        /// </summary>
        /// <param name="attemptsCount">Number of attempts.</param>
        IHeaderOrBuilder WithRetryAttemptsForFailed(int attemptsCount = 3);

        /// <summary>
        /// Build a new http request.
        /// </summary>
        /// <returns></returns>
        IHttpRequest Build();
    }
}