using System;
using System.Net.Http;
using ModernHttpClient;
using Refit;

namespace Localogple.Rest
{
    public static class InTouchMobileRestService
    {
        public static IInTouchMobileClient GetClient()
        {
            var httpClient = new HttpClient(new NativeMessageHandler())
            {
                BaseAddress = new Uri("http://intouchmobileqc.ekeyconnect.com/Service.svc/rest")
            };

            return RestService.For<IInTouchMobileClient>(httpClient);
        }
    }
}