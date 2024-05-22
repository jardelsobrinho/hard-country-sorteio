using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ServerSorterio.Api.Domain;
using ServerSorterio.Api.Hubs;
using ServerSorterio.Api.Infra;

namespace ServerSorterio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SorteioController(IHubContext<NotificationHub, INotificationHubClient> notificationHub) : ControllerBase
    {
        [HttpGet("teste")]
        public IActionResult Teste()
        {
            var listaNumerosSorteados = ConexaoHelper.RetornaNumerosSorteados();
            notificationHub.Clients.All.UpdateNumerosSorteados(listaNumerosSorteados);
            return Ok();
        }

        [HttpGet]
        public ActionResult<IList<NumeroSorteadoEntity>> RetornaNumerosSorteados()
        {
            var listaNumerosSorteados = ConexaoHelper.RetornaNumerosSorteados();
            return Ok(listaNumerosSorteados);
        }

        [HttpPost("{numero}")]
        public IActionResult AdicionaNovoNumero(int numero)
        {
            ConexaoHelper.AdicionaNumero(numero);
            var listaNumerosSorteados = ConexaoHelper.RetornaNumerosSorteados();
            notificationHub.Clients.All.UpdateNumerosSorteados(listaNumerosSorteados);
            return Ok();
        }

        [HttpDelete("{numero}")]
        public IActionResult ExcluiNumero(int numero)
        {
            ConexaoHelper.ExcluirNumeroSorteado(numero);
            var listaNumerosSorteados = ConexaoHelper.RetornaNumerosSorteados();
            notificationHub.Clients.All.UpdateNumerosSorteados(listaNumerosSorteados);
            return Ok();
        }


    }
}
