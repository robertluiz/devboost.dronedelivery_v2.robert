using System.Threading.Tasks;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Interfaces.Services;
using Devboost.DroneDelivery.Domain.Params;

namespace Devboost.DroneDelivery.DomainService
{
    public class PedidoService: IPedidoService
    {
        public async Task<bool> InserirPedido(PedidoParam Pedido)
        {
            var pedido = new PedidoEntity();
            return true;
        }
    }
}