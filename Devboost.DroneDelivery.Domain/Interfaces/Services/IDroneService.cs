using System.Collections.Generic;
using System.Threading.Tasks;
using Devboost.DroneDelivery.Domain.DTOs;

namespace Devboost.DroneDelivery.Domain.Interfaces.Services
{
    public interface IDroneService
    {
        Task<List<ConsultaDronePedidoDTO>> ConsultaDrone();
    }
}