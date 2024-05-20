using Http.Request.Builder.Model;
using Http.Request.Builder.Response;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Http.Request.Builder.Request
{
    internal class HttpRequest : IHttpRequest
    {
        #region Ctor

        private readonly HttpRequestDetail _requestDetail;
        private readonly HttpClient _client = new HttpClient();

        public HttpRequest(HttpRequestDetail requestDetail)
        {
            _requestDetail = requestDetail;
        }

        #endregion

        #region Methods

        private async Task<IHttpResponse> SendRequestAsync()
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
            var response = await _client.SendAsync(request);
            //response.EnsureSuccessStatusCode();
            return new HttpResponse(response.StatusCode, await response.Content.ReadAsStringAsync());

        }
        private async Task<IHttpResponse> SendRequestWithFailedAttemptsTryAsync()
        {            // Send request
            var response = await SendRequestAsync();

            if (IsNeedToTryAgain(response.StatusCode) == false)
                return response;

            if (_requestDetail.FailedAttemptsCount <= 0)
                return response;

            int attemptsDelay = 3 * 1000;
            // Try to send request again.
            for (byte tryCpont = 0; tryCpont < Math.Clamp(_requestDetail.FailedAttemptsCount, 1, 10); tryCpont++)
            {
                // Wait for 1s.
                await Task.Delay(attemptsDelay);
                // Send request.
                response = await SendRequestAsync();
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
        public async Task<IHttpResponse> SendAsync()
        {
            // Send request with try on failed.
            return await SendRequestWithFailedAttemptsTryAsync();
        }

        /// <summary>
        /// Send request async with try on failed. Map response content to Generic class type.
        /// </summary>
        /// <typeparam name="TSuccessContent">Type of content when request was succeded.</typeparam>
        public async Task<IHttpResponse<TSuccessContent>?> SendAsync<TSuccessContent>()
            where TSuccessContent : class
        {
            var response = await SendRequestWithFailedAttemptsTryAsync();
            return new HttpResponse<TSuccessContent>(response.StatusCode, response.Content);
        }

        #endregion
    }
}