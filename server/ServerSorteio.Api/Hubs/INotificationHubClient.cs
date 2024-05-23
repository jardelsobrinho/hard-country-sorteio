using ServerSorteio.Api.Domain;

namespace ServerSorteio.Api.Hubs;


public interface INotificationHubClient
{
    Task UpdateNumerosSorteados(IList<NumeroSorteadoEntity> numerosSorteados);
}
