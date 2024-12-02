using Http.Request.Builder.Exceptions;
using Http.Request.Builder.Extensions;
using Http.Request.Builder.Model;
using Http.Request.Builder.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Http.Request.Builder.Builder
{
    public sealed class RequestBuilder : IHostBuilder, IRouteBuilder, IOptionsBuilder, IHeaderOrBuilder
    {
        private readonly HttpRequestDetail _httpRequestDetail;
        private RequestBuilder(string url, HttpMethod? method = null, HttpClient? httpClient = null)
        {
            _httpRequestDetail = new HttpRequestDetail(url);
            if (method != null)
                _httpRequestDetail.Method = method;
            if(httpClient != null)
                _httpRequestDetail.HttpClient = httpClient;
        }

        /// <summary>
        /// Create new instance of RequestBuilder.
        /// </summary>
        /// <returns>RequestBuilder instance object.</returns>
        public static IRouteBuilder Create(string url, HttpMethod? method = null, HttpClient? httpClient = null)
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
                if (prop is null)
                    continue;
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
                string? valueString = value != null ?
                        TypeDescriptor.GetConverter(prop.PropertyType)
                        .ConvertToInvariantString(value) :
                        string.Empty;
                content.Add(new StringContent(valueString is null ? string.Empty : valueString), key);
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
            return WithContentAsRaw(JsonSerializer.Serialize(data));
        }

        #endregion

        public IHeaderOrBuilder WithRetryAttemptsForFailed(int attemptsCount = 3)
        {
            _httpRequestDetail.FailedAttemptsOptions.MaxRetries = Math.Clamp(attemptsCount, 1, 20);

            return this;
        }

        public IHeaderOrBuilder WithRetryAttemptsForFailed(Action<FailedRetryAttemptOptionsModel>? options = null)
        {
            _httpRequestDetail.isTryForFailEnabled = true;

            // Initialize a default instance
            var retryOptions = new FailedRetryAttemptOptionsModel();

            // If options is not null, invoke it to configure the retryOptions
            options?.Invoke(retryOptions);

            _httpRequestDetail.FailedAttemptsOptions = retryOptions;

            return this;
        }

        public IHttpRequest Build()
            => new HttpRequest(_httpRequestDetail);
    }
}