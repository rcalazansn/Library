using Library.Application.ViewModel;
using MediatR;

namespace Library.Application.Queries.ViaCep
{
    public class GetViaCepByCepQuery : IRequest<ViaCepModel>
    {
        public GetViaCepByCepQuery(string cep) => Cep = cep;

        public string Cep { get; private set; }
    }

    public class GetViaCepByCepClientFactoryQuery : IRequest<ViaCepModel>
    {
        public GetViaCepByCepClientFactoryQuery(string cep) => Cep = cep;

        public string Cep { get; private set; }
    }
}
