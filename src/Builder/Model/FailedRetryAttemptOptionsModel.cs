using System;

namespace Http.Request.Builder.Model
{
    public class RetryPolicyOptionsModel
    {
        /// <summary>
        /// Default is 1.
        /// </summary>
        public int MaxRetries { get; set; } = 1;

        /// <summary>
        /// Default is from 1 second.
        /// </summary>
        public TimeSpan Delay { get; set; } = TimeSpan.FromSeconds(1);
    }
}