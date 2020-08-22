using System;
using System.Threading.Tasks;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Params;

namespace Devboost.DroneDelivery.Domain.Interfaces.Services
{
    public interface IPedidoService
    {
        Task<bool> InserirPedido(PedidoParam Pedido);
        Task<Guid> IdPedidoPorIdDrone(int DroneId);
    }
}