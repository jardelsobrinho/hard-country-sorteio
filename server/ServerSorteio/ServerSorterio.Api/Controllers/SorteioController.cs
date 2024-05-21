using Microsoft.AspNetCore.Mvc;
using ServerSorterio.Api.Infra;

namespace ServerSorterio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SorteioController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            ConexaoHelper.CriarBancoSQLite();
            return Ok();
        }

        [HttpPost("{numero}")]
        public IActionResult AdicionaNovoNumero(int numero)
        {
            ConexaoHelper.AdicionaNumero(numero);
            return Ok();
        }

        [HttpDelete("{numero}")]
        public IActionResult ExcluiNumero(int numero)
        {
            ConexaoHelper.ExcluirNumeroSorteado(numero);
            return Ok();
        }
    }
}
