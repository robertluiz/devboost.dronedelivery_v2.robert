using System.Collections.Generic;
using System.Threading.Tasks;
using Devboost.DroneDelivery.Domain.DTOs;
using Devboost.DroneDelivery.Domain.Entities;

namespace Devboost.DroneDelivery.Domain.Interfaces.Queries
{
    public interface IDroneQuery
    {
        Task<List<ConsultaDronePedidoDTO>> ConsultaDrone();
        Task<ConsultaDronePedidoDTO> RetornaConsultaDronePedido(DroneEntity drone);
    }
}