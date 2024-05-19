namespace Http.Request.Builder.Builder
{
    public interface IHeaderOption
    {
        /// <summary>
        /// Appends a key value pair to the Http Client.
        /// </summary>
        /// <param name="key">Header key</param>
        /// <param name="value">Header value</param>
        /// <returns>Optional pipeline step to append additional headers</returns>
        IHeaderOrBuilder WithHeader(string key, string value);

        /// <summary>
        /// Appends a key value pair to the Http Client returned from a function.
        /// </summary>
        /// <param name="headerFunc">Header key</param>
        /// <returns>Optional pipeline step to append additional headers</returns>
        IHeaderOrBuilder WithHeader(Func<KeyValuePair<string, string>?> headerFunc);

    }
}