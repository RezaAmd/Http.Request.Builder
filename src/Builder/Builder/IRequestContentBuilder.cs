namespace HttpRequestBuilder.Builder
{
    public interface IRequestContentBuilder
    {
        /// <summary>
        /// Set form data in request content.
        /// </summary>
        /// <param name="data">Content data as key value pair.</param>
        IHeaderOrBuilder WithContentAsFormData(IList<KeyValuePair<string, string>> form);

        /// <summary>
        /// Set x-www-form-urlencoded in request content.
        /// </summary>
        /// <param name="data">Key value pair data.</param>
        IHeaderOrBuilder WithContentAsFormUrlEncoded(IList<KeyValuePair<string, string>> form);

        /// <summary>
        /// Set raw in request content. (default media type is json)
        /// </summary>
        /// <param name="raw">Serialized data.</param>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        IHeaderOrBuilder WithContentAsRaw(string raw, string mediaType = "application/json");

        /// <summary>
        /// Set raw in request content. Serialize data at first then add to request content as serialized json.
        /// </summary>
        /// <typeparam name="TData">Request content data to send from body.</typeparam>
        /// <param name="data">Request content data to send from body.</param>
        IHeaderOrBuilder WithDataFromBodyAsJson<TData>(TData data);
    }
}
