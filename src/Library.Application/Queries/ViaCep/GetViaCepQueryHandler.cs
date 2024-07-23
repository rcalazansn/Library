using Library.Application.Http;
using Library.Application.Http.ViaCep;
using Library.Application.Http.ViaCep.HttpClientFactory;
using Library.Application.ViewModel;
using Library.Core.Application;
using Library.Core.Notification;
using MediatR;
using Microsoft.Extensions.Logging;
using Refit;
using System.Diagnostics;

namespace Library.Application.Queries.ViaCep
{
    public class GetViaCepQueryHandler : BaseHandler,
        IRequestHandler<GetViaCepByCepQuery, ViaCepModel>,
        IRequestHandler<GetViaCepByCepClientFactoryQuery, ViaCepModel>
    {
        private readonly ILogger<GetViaCepQueryHandler> _logger;
        private readonly IViaCepClientFactory _viaCepClientFactory;
        public GetViaCepQueryHandler
        (
            ILogger<GetViaCepQueryHandler> logger,
            INotifier notifier,
            IViaCepClientFactory viaCepClientFactory
        ) : base(notifier)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _viaCepClientFactory = viaCepClientFactory ?? throw new ArgumentNullException(nameof(viaCepClientFactory));
        }

        public async Task<ViaCepModel> Handle(GetViaCepByCepQuery request, CancellationToken cancellationToken)
        {
            var watch = Stopwatch.StartNew();

            var viaCepClient = RestService.For<IViaCepClient>(ProviderBase.GetUrl("ViaCEP"));

            if (viaCepClient == null)
            {
                Notify("Unable to instantiate the class ViaCepClient");
                return null;
            }

            var viaCep = await viaCepClient.GetAddressAsync(request.Cep);

            if (viaCep == null || viaCep?.Cep == null)
            {
                Notify($"CEP:{request.Cep} not found.");
                return null;
            }

            var viaCepModel = new ViaCepModel
            (
                 cep: viaCep.Cep,
                 logradouro: viaCep.Logradouro,
                 complemento: viaCep.Complemento,
                 bairro: viaCep.Bairro,
                 localidade: viaCep.Localidade,
                 uf: viaCep.Uf
            );

            watch.Stop();

            _logger.LogDebug
            (
               $"{viaCepModel.Cep} successfully " +
               $"ElapsedMilliseconds={watch.ElapsedMilliseconds}"
            );
            return viaCepModel;
        }

        public async Task<ViaCepModel> Handle(GetViaCepByCepClientFactoryQuery request, CancellationToken cancellationToken)
        {
            var watch = Stopwatch.StartNew();

            var viaCep = await _viaCepClientFactory.GetAddressAsync(request.Cep);

            if (viaCep == null || viaCep?.Cep == null)
            {
                Notify($"CEP:{request.Cep} not found.");
                return null;
            }

            var viaCepModel = new ViaCepModel
            (
                 cep: viaCep.Cep,
                 logradouro: viaCep.Logradouro,
                 complemento: viaCep.Complemento,
                 bairro: viaCep.Bairro,
                 localidade: viaCep.Localidade,
                 uf: viaCep.Uf
            );

            watch.Stop();

            _logger.LogDebug
            (
               $"{viaCepModel.Cep} successfully " +
               $"ElapsedMilliseconds={watch.ElapsedMilliseconds}"
            );
            return viaCepModel;
        }
    }
}
