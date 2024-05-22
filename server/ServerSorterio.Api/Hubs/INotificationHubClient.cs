using ServerSorterio.Api.Domain;

namespace ServerSorterio.Api.Hubs;


public interface INotificationHubClient
{
    Task UpdateNumerosSorteados(IList<NumeroSorteadoEntity> numerosSorteados);
}
