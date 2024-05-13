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
            var request = new HttpRequestMessage(HttpMethod.Get, _requestDetail.Uri.ToString());
            var collection = new List<KeyValuePair<string, string>>();
            collection.Add(new("ttt", "ooooo"));
            var content = new FormUrlEncodedContent(collection);
            request.Content = content;
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            HttpResponse httpResponse = new();

            throw new NotImplementedException();
        }
    }
}