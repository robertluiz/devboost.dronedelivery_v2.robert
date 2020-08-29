using Devboost.DroneDelivery.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace Devboost.DroneDelivery.DomainService
{
    public class EntregaService : IEntregaService
    {
        private readonly IDroneService _droneService;        

        public EntregaService(IDroneService droneService)
        {
            _droneService = droneService;            
        }

        public async Task Inicia()
        {
            await _droneService.LiberaDrone();
        }        
    }
}