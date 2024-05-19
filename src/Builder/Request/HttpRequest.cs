using HttpRequestBuilder.Response;

namespace HttpRequestBuilder.Request
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

        public async Task<IHttpResponse> SendAsync()
        {
            var request = new HttpRequestMessage(_requestDetail.Method, _requestDetail.Uri.ToString());
            // Header
            foreach (var header in _requestDetail.Headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
            // Authorization
            if(_requestDetail.Authentication != null)
            request.Headers.Authorization = _requestDetail.Authentication;
            // Content
            if (_requestDetail.Content != null)
                request.Content = _requestDetail.Content;
            // Send request.
            var response = await _client.SendAsync(request);
            //response.EnsureSuccessStatusCode();
            return new HttpResponse(response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}