using Http.Request.Builder.Model;
using Http.Request.Builder.Request;
using System;

namespace Http.Request.Builder.Builder
{
    public interface IRequestBuilder
    {

        [Obsolete("Use the method accepting 'Action<FailedRetryAttemptOptionsModel>' for more flexible and detailed retry configuration.")]
        /// <summary>
        /// If the response is failed, it will try again. (Retry Pattern)
        /// </summary>
        /// <param name="attemptsCount">Number of attempts.</param>
        IHeaderOrBuilder WithRetryAttemptsForFailed(int attemptsCount);

        /// <summary>
        /// If the response is failed, it will try again. (Retry Pattern).
        /// The default value of retry count is 1 with 1s delay.
        /// </summary>
        /// <param name="options">
        /// An optional action to configure the retry attempt settings using a <see cref="FailedRetryAttemptOptionsModel"/> instance.
        /// If no action is provided, default retry attempt settings will be used.
        /// </param>
        /// <returns></returns>
        IHeaderOrBuilder WithRetryAttemptsForFailed(Action<FailedRetryAttemptOptionsModel>? options = null);

        /// <summary>
        /// Build a new http request.
        /// </summary>
        /// <returns></returns>
        IHttpRequest Build();
    }
}