using Microsoft.AspNetCore.SignalR;
using ServerSorterio.Api.Domain;

namespace ServerSorterio.Api.Hubs;

public class NotificationHub : Hub<INotificationHubClient>
{
    public async Task UpdateNumerosSorteados(IList<NumeroSorteadoEntity> numerosSorteados)
    {
        await Clients.All.UpdateNumerosSorteados(numerosSorteados);
    }
}
