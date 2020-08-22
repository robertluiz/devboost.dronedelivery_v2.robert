using System.Collections.Generic;
using System.Threading.Tasks;
using Devboost.DroneDelivery.Domain.DTOs;
using Devboost.DroneDelivery.Domain.Entities;

namespace Devboost.DroneDelivery.Domain.Interfaces.Services
{
    public interface IDroneService
    {
        Task<List<ConsultaDronePedidoDTO>> ConsultaDrone();
        Task<DroneEntity> SelecionarDrone();
        Task AtualizaDrone(DroneEntity drone);
    }
}