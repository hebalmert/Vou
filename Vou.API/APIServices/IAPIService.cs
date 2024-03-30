using Vou.Shared.Responses;

namespace Vou.API.APIServices
{
    public interface IAPIService
    {
        Task<Response> GetListAsync<T>(string servicePrefix, string controller);
    }
}
