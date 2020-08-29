using System;
using System.Threading.Tasks;
using Devboost.DroneDelivery.Domain.Enums;
using Devboost.DroneDelivery.Domain.Interfaces.Services;
using Devboost.DroneDelivery.Domain.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Devboost.DroneDelivery.Api.Controllers
{
    [Route("v1/entrega")]
    [ApiController]
    public class EntregaController : Controller
    {
        private readonly IEntregaService _entregaService;

        public EntregaController(IEntregaService entregaService)
        {
            _entregaService = entregaService;
        }

        [HttpPost("Inicia")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult>  IniciaEntrega()
        {
            try
            {
               await _entregaService.Inicia();

               return Ok("Entrega iniciada!");
            }
            catch (Exception e)
            {
              return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }

        }
    }
}