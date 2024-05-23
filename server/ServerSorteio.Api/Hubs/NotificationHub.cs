using Microsoft.AspNetCore.SignalR;
using ServerSorteio.Api.Domain;

namespace ServerSorteio.Api.Hubs;

public class NotificationHub : Hub<INotificationHubClient>
{
    public async Task UpdateNumerosSorteados(IList<NumeroSorteadoEntity> numerosSorteados)
    {
        await Clients.All.UpdateNumerosSorteados(numerosSorteados);
    }
}
