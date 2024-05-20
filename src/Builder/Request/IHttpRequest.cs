using Http.Request.Builder.Response;
using System.Threading.Tasks;

namespace Http.Request.Builder.Request
{
    public interface IHttpRequest
    {
        Task<IHttpResponse> SendAsync();
    }
}