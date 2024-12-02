using Http.Request.Builder.Model;
using Http.Request.Builder.Response;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Http.Request.Builder.Request
{
    internal class HttpRequest : IHttpRequest
    {
        #region Ctor

        private readonly HttpRequestDetail _requestDetail;
        private readonly HttpClient _client = new HttpClient();
        private int attemptDelayPerSecond = 1;
        private byte attemptsCount = 0;

        public HttpRequest(HttpRequestDetail requestDetail)
        {
            _requestDetail = requestDetail;
        }

        public HttpRequest(HttpRequestDetail requestDetail, HttpClient httpClient)
        {
            _requestDetail = requestDetail;
            _client = httpClient;
        }

        #endregion

        #region Methods

        private async Task<IHttpResponse> SendRequestAsync(CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(_requestDetail.Method, _requestDetail.Uri.ToString());
            // Header
            foreach (var header in _requestDetail.Headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
            // Authorization
            if (_requestDetail.Authentication != null)
                request.Headers.Authorization = _requestDetail.Authentication;
            // Content
            if (_requestDetail.Content != null)
                request.Content = _requestDetail.Content;
            // Send request.
            var response = await _client.SendAsync(request, cancellationToken);
            //response.EnsureSuccessStatusCode();
            return new HttpResponse(response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));

        }

        private async Task<IHttpResponse> SendRequestWithFailedAttemptsTryAsync(CancellationToken cancellationToken = default)
        {
            // Send request
            var response = await SendRequestAsync(cancellationToken);

            if (IsNeedToTryAgain(response.StatusCode) == false)
                return response;

            if (_requestDetail.FailedAttemptsCount <= 0)
                return response;

            // Try to send request again.
            for (attemptsCount = 0; attemptsCount < Math.Clamp(_requestDetail.FailedAttemptsCount, 1, 10); attemptsCount++)
            {
                await HandleDelayOfAttemptsByStatusCode(response.StatusCode);
                // Send request.
                response = await SendRequestAsync(cancellationToken);
                if (!IsNeedToTryAgain(response.StatusCode))
                    return response;
            }

            return response;
        }

        private async Task HandleDelayOfAttemptsByStatusCode(HttpStatusCode statusCode)
        {
            // Determining of delay
            DeterminingDelayByStatusCodeAndFailedAttemptsCount(statusCode);
            await Task.Delay(attemptDelayPerSecond * 1000);
        }

        private void DeterminingDelayByStatusCodeAndFailedAttemptsCount(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpStatusCode.TooManyRequests:
                    attemptDelayPerSecond = 1;
                    if (attemptsCount > 3)
                        attemptDelayPerSecond = 3;
                    if (attemptsCount > 6)
                        attemptDelayPerSecond = 5;
                    break;
                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.NotImplemented:
                case HttpStatusCode.BadGateway:
                case HttpStatusCode.ServiceUnavailable:
                case HttpStatusCode.GatewayTimeout:
                case HttpStatusCode.HttpVersionNotSupported:
                case HttpStatusCode.VariantAlsoNegotiates:
                case HttpStatusCode.InsufficientStorage:
                case HttpStatusCode.LoopDetected:
                case HttpStatusCode.NotExtended:
                case HttpStatusCode.NetworkAuthenticationRequired:
                    attemptDelayPerSecond = 1;
                    if (attemptsCount > 6)
                        attemptDelayPerSecond = 5;
                    else if (attemptsCount > 3)
                        attemptDelayPerSecond = 3;
                    break;
                default:
                    attemptDelayPerSecond = 1;
                    break;
            }
        }

        private bool IsNeedToTryAgain(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.NotImplemented:
                case HttpStatusCode.BadGateway:
                case HttpStatusCode.ServiceUnavailable:
                case HttpStatusCode.GatewayTimeout:
                case HttpStatusCode.HttpVersionNotSupported:
                case HttpStatusCode.VariantAlsoNegotiates:
                case HttpStatusCode.InsufficientStorage:
                case HttpStatusCode.LoopDetected:
                case HttpStatusCode.NotExtended:
                case HttpStatusCode.NetworkAuthenticationRequired:
                case HttpStatusCode.TooManyRequests:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Send request async with try on failed.
        /// </summary>
        /// <returns>Http response</returns>
        public async Task<IHttpResponse> SendAsync(CancellationToken cancellationToken = default)
        {
            // Send request with try on failed.
            return await SendRequestWithFailedAttemptsTryAsync(cancellationToken);
        }

        /// <summary>
        /// Send request async with try on failed. Map response content to Generic class type.
        /// </summary>
        /// <typeparam name="TSuccessContent">Type of content when request was succeded.</typeparam>
        public async Task<IHttpResponse<TSuccessContent>?> SendAsync<TSuccessContent>(CancellationToken cancellationToken = default)
            where TSuccessContent : class
        {
            var response = await SendRequestWithFailedAttemptsTryAsync(cancellationToken);
            return new HttpResponse<TSuccessContent>(response.StatusCode, response.Content);
        }

        #endregion
    }
}