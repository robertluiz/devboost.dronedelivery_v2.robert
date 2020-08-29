using Devboost.DroneDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Devboost.DroneDelivery.Domain.Interfaces.Queries
{
    public interface IPedidoQuery
    {        
        Task<PedidoEntity> GetPedidoByIdDrone(Guid DroneId);        
        Task<List<PedidoEntity>> GetAll();
    }
}