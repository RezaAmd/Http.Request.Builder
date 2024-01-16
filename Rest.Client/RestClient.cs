using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Rest.Client
{
    public class RestClient
    {
        #region Fields

        private UriBuilder _uriBuilder;
        private HttpMethod _method = HttpMethod.Get;
        private IDictionary<string, string> _headers = new Dictionary<string, string>();
        private IDictionary<string, string> _parameters = new Dictionary<string, string>();

        #endregion

        #region Ctor

        public RestClient(string url)
        {
            if (url == null) throw new ArgumentNullException("url cannot be null.");
            _uriBuilder = new UriBuilder(url);
        }

        #endregion

        #region Methods

        public RestClient SetMethod(HttpMethod method)
        {
            _method = method;
            return this;
        }
        /// <summary>
        /// Add query parameter to url.
        /// </summary>
        /// <param name="key">parameter key.</param>
        /// <param name="value">parameter value.</param>
        /// <returns>Instance of RestClient.</returns>
        public RestClient AddQueryParameter(string key, string value)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException("key cannot be null.");
            _parameters.Add(key, value);
            return this;
        }
        /// <summary>
        /// Add query parameter to url.
        /// </summary>
        /// <param name="parameters">parameters as dictionary.</param>
        /// <returns>Instance of RestClient.</returns>
        public RestClient AddQueryParameter(IDictionary<string, string> parameters)
        {
            _parameters = parameters;
            return this;
        }
        /// <summary>
        /// Add header to request.
        /// </summary>
        /// <param name="key">Header key.</param>
        /// <param name="value">Header value</param>
        /// <returns>Instance of RestClient.</returns>
        public RestClient AddHeader(string key, string value)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException("header key cannot be null.");
            _headers.Add(key, value);
            return this;
        }
        /// <summary>
        /// Send request.
        /// </summary>
        /// <returns>Tuple of (Status Code, Content) after request sent.</returns>
        public async Task<(HttpStatusCode StatusCode, string Content)> SendAsync(CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
                return (HttpStatusCode.NoContent, null);

            // Append parameters
            #region Parameters

            foreach (var parameter in _parameters)
            {
                var query = HttpUtility.ParseQueryString(_uriBuilder.Query);
                query[parameter.Key] = parameter.Value;
                _uriBuilder.Query = query.ToString();
            }

            #endregion

            // Initialize client and request.
            var client = new HttpClient();
            var request = new HttpRequestMessage(_method, _uriBuilder.ToString());

            // Append header
            #region Headers

            foreach (var header in _headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }

            #endregion

            var response = await client.SendAsync(request);
            //response.EnsureSuccessStatusCode();
            return (response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        #endregion
    }
}