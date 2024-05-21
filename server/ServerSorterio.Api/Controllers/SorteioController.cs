using Microsoft.AspNetCore.Mvc;
using ServerSorterio.Api.Domain;
using ServerSorterio.Api.Infra;

namespace ServerSorterio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SorteioController : ControllerBase
    {
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
