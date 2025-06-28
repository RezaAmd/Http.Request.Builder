namespace Http.Request.Builder.Builder
{
    public interface IBasicAuthBuilder
    {
        /// <summary>
        /// Adds the basic authentication to header.
        /// </summary>
        /// <param name="username">Username for authentication.</param>
        /// <param name="password">Password for authentication.</param>
        /// <returns>Option step in the builder pipeline</returns>
        IOptionsBuilder WithBasicAuthentication(string username, string password);
    }
}