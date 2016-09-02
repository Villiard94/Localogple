using System.Threading.Tasks;
using Localogple.Rest.Requests;
using Localogple.Rest.Responses;
using Refit;

namespace Localogple.Rest
{
    public interface IInTouchMobileClient
    {
        [Post("/SetLocation")]
        Task<Response> LogLocation(LogLocationRequest request);
    }
}