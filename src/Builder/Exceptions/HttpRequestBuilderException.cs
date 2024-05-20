using System;

namespace Http.Request.Builder.Exceptions
{
    public class HttpRequestBuilderException : Exception
    {
        public HttpRequestBuilderException(string message)
            : base(message)
        {
        }
    }
}