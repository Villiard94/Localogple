using System.Threading.Tasks;
using Localogple.Rest.Requests;
using Refit;

namespace Localogple.Rest
{
    public interface IInTouchMobileClient
    {
        [Post("/SetLocation")]
        Task LogLocation(LogLocationRequest request);
    }
}