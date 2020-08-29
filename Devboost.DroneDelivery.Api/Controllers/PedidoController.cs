using Devboost.DroneDelivery.Domain.Interfaces.Commands;
using Devboost.DroneDelivery.Domain.Interfaces.Queries;
using Devboost.DroneDelivery.Domain.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Devboost.DroneDelivery.Api.Controllers
{
    [Route("v1/pedido")]
    [ApiController]
    public class PedidoController : Controller
    {
        private readonly IPedidoCommand _pedidoCommand;
        private readonly IPedidoQuery _pedidoQuery;

        public PedidoController(IPedidoCommand pedidoCommand, IPedidoQuery pedidoQuery)
        {
            _pedidoCommand = pedidoCommand;
            _pedidoQuery = pedidoQuery;
        }

        [HttpGet("criados")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var lista = await _pedidoQuery.GetAll();
                if (lista.Count.Equals(0)) return NotFound();
                return Ok(lista);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpPost]
        [Authorize(Roles = "Comprador,Administrador")]
        public async Task<IActionResult>  ReceberPedido(PedidoParam pedido)
        {
            try
            {
               var resultado = await _pedidoCommand.InserirPedido(pedido);
               if (!resultado)
                   return BadRequest("Pedido não aceito");
               return Ok("Pedido realizado com sucesso!");
            }
            catch (Exception e)
            {
              return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }

        }
    }
}