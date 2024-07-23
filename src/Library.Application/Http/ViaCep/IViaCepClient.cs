using Refit;

namespace Library.Application.Http.ViaCep
{
    public interface IViaCepClient
    {
        [Get("/ws/{cep}/json")]
        Task<ViaCepResponse> GetAddressAsync(string cep);
    }
}
