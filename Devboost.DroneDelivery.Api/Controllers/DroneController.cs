using System;
using System.Threading.Tasks;
using Devboost.DroneDelivery.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Devboost.DroneDelivery.Api.Controllers
{
    [Route("v1/drone")]
    [ApiController]
    public class DroneController : Controller
    {
        private readonly IDroneService _droneService;

        public DroneController(IDroneService droneService)
        {
            _droneService = droneService;
        }

        [HttpGet("situacao")]
        public async Task<IActionResult>  SituacaoDrone()
        {
            try
            {
                var lista = await _droneService.ConsultaDrone();
                if(lista.Count.Equals(0)) return NotFound();
                return Ok(lista);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }
    }
}