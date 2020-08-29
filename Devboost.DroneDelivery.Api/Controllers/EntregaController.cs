using Devboost.DroneDelivery.Domain.Interfaces.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Devboost.DroneDelivery.Api.Controllers
{
    [Route("v1/entrega")]
    [ApiController]
    public class EntregaController : Controller
    {
        private readonly IEntregaCommand _entregaCommand;

        public EntregaController(IEntregaCommand entregaCommand)
        {
            _entregaCommand = entregaCommand;
        }

        [HttpPost("Inicia")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult>  IniciaEntrega()
        {
            try
            {
               await _entregaCommand.Inicia();

               return Ok("Entrega iniciada!");
            }
            catch (Exception e)
            {
              return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }

        }
    }
}