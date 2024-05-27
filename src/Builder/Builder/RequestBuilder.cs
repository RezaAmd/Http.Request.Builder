using Http.Request.Builder.Exceptions;
using Http.Request.Builder.Model;
using Http.Request.Builder.Request;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.ComponentModel;
using Http.Request.Builder.Extensions;

namespace Http.Request.Builder.Builder
{
    public sealed class RequestBuilder : IHostBuilder, IRouteBuilder, IOptionsBuilder, IHeaderOrBuilder
    {
        private readonly HttpRequestDetail _httpRequestDetail;
        private RequestBuilder(string url, HttpMethod method = null)
        {
            _httpRequestDetail = new HttpRequestDetail(url);
            if (method != null)
                _httpRequestDetail.Method = method;
        }

        /// <summary>
        /// Create new instance of RequestBuilder.
        /// </summary>
        /// <returns>RequestBuilder instance object.</returns>
        public static IRouteBuilder Create(string url, HttpMethod method = null)
            => new RequestBuilder(url, method);

        #region URI & Authorization

        /// <summary>
        /// Authentication by bearer JWT token.
        /// </summary>
        /// <param name="token">JWT token</param>
        public IOptionsBuilder WithBearerToken(string token)
        {
            _httpRequestDetail.Authentication = new AuthenticationHeaderValue("Bearer", token);
            return this;
        }

        #endregion

        #region Headers

        /// <summary>
        /// Add extra header for request.
        /// </summary>
        public IHeaderOrBuilder WithHeader(string key, string value)
        {
            _httpRequestDetail.Headers.Add(key, value);
            return this;
        }
        /// <summary>
        /// Add extra header for request.
        /// </summary>
        public IHeaderOrBuilder WithHeader(Func<KeyValuePair<string, string>?> headerFunc)
        {
            var header = (headerFunc?.Invoke()) ?? throw new HttpRequestBuilderException(nameof(WithHeader));
            _httpRequestDetail.Headers.Add(header.Key, header.Value);
            return this;
        }

        #endregion

        #region Body Content

        public IHeaderOrBuilder WithHttpContent(HttpContent content)
        {
            _httpRequestDetail.Content = content;
            return this;
        }

        public IHeaderOrBuilder WithContentAsFormData<TData>(TData form)
        {
            var content = new MultipartFormDataContent();

            foreach (var prop in typeof(TData).GetProperties())
            {
                // get properties we can read and write to
                if (!prop.CanRead || !prop.CanWrite)
                    continue;
                if (!TypeDescriptor.GetConverter(prop.PropertyType)
                    .CanConvertFrom(typeof(string)))
                    continue;
                var key = prop.GetJsonPropertyValue();
                if (string.IsNullOrEmpty(key))
                    key = prop.Name;
                var value = prop.GetValue(form, null);
                string valueString = (value != null ?
                        TypeDescriptor.GetConverter(prop.PropertyType)
                        .ConvertToInvariantString(value) :
                        string.Empty);

                content.Add(new StringContent(valueString), key);
            }

            _httpRequestDetail.Content = content;
            return this;
        }

        public IHeaderOrBuilder WithContentAsFormData(IList<KeyValuePair<string, string>> form)
        {
            var content = new MultipartFormDataContent();
            foreach (var _data in form)
            {
                content.Add(new StringContent(_data.Key), _data.Value);
            }
            _httpRequestDetail.Content = content;
            return this;
        }

        public IHeaderOrBuilder WithContentAsFormUrlEncoded(IList<KeyValuePair<string, string>> form)
        {
            _httpRequestDetail.Content = new FormUrlEncodedContent(form);
            return this;
        }

        public IHeaderOrBuilder WithContentAsRaw(string raw, string mediaType = "application/json")
        {
            _httpRequestDetail.Content = new StringContent(raw, null, mediaType);
            return this;
        }

        public IHeaderOrBuilder WithDataFromBodyAsJson<TData>(TData data)
        {
            _httpRequestDetail.Content = new StringContent(JsonConvert.SerializeObject(data), null, "application/json");
            return this;
        }

        #endregion

        public IHeaderOrBuilder WithRetryAttemptsForFailed(int attemptsCount = 3)
        {
            _httpRequestDetail.FailedAttemptsCount = Math.Clamp(attemptsCount, 1, 20);
            return this;
        }

        public IHttpRequest Build()
            => new HttpRequest(_httpRequestDetail);
    }
}