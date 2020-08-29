using System;
using System.Threading.Tasks;
using Devboost.DroneDelivery.Domain.Interfaces.Services;
using Devboost.DroneDelivery.Domain.Params;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Devboost.DroneDelivery.Api.Controllers
{
    [Route("v1/pedido")]
    [ApiController]
    public class PedidoController : Controller
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet("criados")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var lista = await _pedidoService.GetAll();
                if (lista.Count.Equals(0)) return NotFound();
                return Ok(lista);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult>  ReceberPedido(PedidoParam pedido)
        {
            try
            {
               var resultado = await _pedidoService.InserirPedido(pedido);
               if (!resultado)
                   return BadRequest("Pedido não aceito");
               return Ok();
            }
            catch (Exception e)
            {
              return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }

        }
    }
}