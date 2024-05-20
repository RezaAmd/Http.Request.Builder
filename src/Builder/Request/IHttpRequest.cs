using Http.Request.Builder.Response;
using System.Threading;
using System.Threading.Tasks;

namespace Http.Request.Builder.Request
{
    public interface IHttpRequest
    {
        /// <summary>
        /// Send request async with try on failed.
        /// </summary>
        /// <returns>Http response</returns>
        Task<IHttpResponse> SendAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Send request async with try on failed. Map response content to Generic class type.
        /// </summary>
        /// <typeparam name="TSuccessContent">Type of content when request was succeded.</typeparam>
        Task<IHttpResponse<TSuccessContent>?> SendAsync<TSuccessContent>(CancellationToken cancellationToken = default)
                    where TSuccessContent : class;
    }
}