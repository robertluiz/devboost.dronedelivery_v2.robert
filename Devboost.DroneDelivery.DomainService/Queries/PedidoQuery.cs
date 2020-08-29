using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Interfaces.Queries;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Devboost.DroneDelivery.DomainService.Queries
{
    public class PedidoQuery : IPedidoQuery    {
        
        private readonly IPedidosRepository _pedidosRepository;

        public PedidoQuery(IPedidosRepository pedidosRepository)
        {
            _pedidosRepository = pedidosRepository;
        }

        public async Task<PedidoEntity> GetPedidoByIdDrone(Guid droneId)
        {
            return await _pedidosRepository.GetSingleByDroneID(droneId);
        }

        public async Task<List<PedidoEntity>> GetAll()
        {
            return await _pedidosRepository.GetAll();
        }
    }
}