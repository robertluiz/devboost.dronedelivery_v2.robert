using Devboost.DroneDelivery.Domain.DTOs;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Interfaces.Queries;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devboost.DroneDelivery.DomainService.Queries
{
    public class DroneQuery : IDroneQuery
    {
        private readonly IDronesRepository _dronesRepository;
        private readonly IPedidosRepository _pedidosRepository;
        public DroneQuery(IDronesRepository dronesRepository, IPedidosRepository pedidosRepository)
        {
            _dronesRepository = dronesRepository;
            _pedidosRepository = pedidosRepository;
        }

        public async Task<List<ConsultaDronePedidoDTO>> ConsultaDrone()
        {
            var listaDrones = await _dronesRepository.GetAll();
            //AtualizaStatusDrones(listaDrones); Por enquanto os Drones nao serão atualizados via consulta de pedido, talvez um fila será responável por fazer essa atualização
            var drones = await _dronesRepository.GetAll();
            return drones.Select(async d => await RetornaConsultaDronePedido(d))
                .ToList()
                .Select(c => c.Result)
                .ToList();

        }

        public async Task<ConsultaDronePedidoDTO> RetornaConsultaDronePedido(DroneEntity drone)
        {

            var pedidos = await _pedidosRepository.GetByDroneID(drone.Id);

            return new ConsultaDronePedidoDTO
            {

                IdDrone = drone.Id,
                Situacao = drone.Status.ToString(),
                Pedidos = pedidos
            };
        }        
    }
}