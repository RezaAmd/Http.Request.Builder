namespace HttpRequestBuilder.Builder
{
    public interface IBearerTokenAuthBuilder
    {
        /// <summary>
        /// Adds the bearer token header and advances to the next step in the pipeline.
        /// </summary>
        /// <param name="token">JWT Token</param>
        /// <returns>Option step in the builder pipeline</returns>
        IOptionsBuilder WithBearerToken(string token);
    }
}