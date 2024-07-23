namespace Library.Application.Http.ViaCep.HttpClientFactory
{
    public interface IViaCepClientFactory
    {
        Task<ViaCepResponse> GetAddressAsync(string cep);
    }
}
