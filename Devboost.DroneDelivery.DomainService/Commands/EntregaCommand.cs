using Devboost.DroneDelivery.Domain.Interfaces.Commands;
using Devboost.DroneDelivery.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace Devboost.DroneDelivery.DomainService
{
    public class EntregaCommand : IEntregaCommand
    {
        private readonly IDroneCommand _droneCommand;        

        public EntregaCommand(IDroneCommand droneCommand)
        {
            _droneCommand = droneCommand;            
        }

        public async Task Inicia()
        {
            await _droneCommand.LiberaDrone();
        }        
    }
}