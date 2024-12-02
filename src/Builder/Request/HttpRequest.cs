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
        #region Fields & Constructor

        private readonly HttpRequestDetail _requestDetail;
        public int FailedAttemptsCount { get; private set; } = 0;
        public HttpRequest(HttpRequestDetail requestDetail)
        {
            _requestDetail = requestDetail;
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
            var response = await _requestDetail.HttpClient.SendAsync(request, cancellationToken);
            //response.EnsureSuccessStatusCode();
            return new HttpResponse(response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken), FailedAttemptsCount);

        }

        private async Task<IHttpResponse> SendRequestWithFailedAttemptsTryAsync(CancellationToken cancellationToken = default)
        {
            // Send request
            var response = await SendRequestAsync(cancellationToken);

            if (IsNeedToTryAgain(response.StatusCode) == false || _requestDetail.isTryForFailEnabled == false)
                return response;

            if (_requestDetail.FailedAttemptsOptions.MaxRetries <= 0)
                return response;

            // Try to send request again.
            for (FailedAttemptsCount = 0; FailedAttemptsCount <= _requestDetail.FailedAttemptsOptions.MaxRetries; FailedAttemptsCount++)
            {
                // Wait for delay.
                await Task.Delay(_requestDetail.FailedAttemptsOptions.Delay);

                // Send request.
                response = await SendRequestAsync(cancellationToken);

                if (!IsNeedToTryAgain(response.StatusCode))
                    return response;
            }

            return response;
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