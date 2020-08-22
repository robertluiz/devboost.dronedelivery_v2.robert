using Devboost.DroneDelivery.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Devboost.DroneDelivery.Domain.Interfaces.Repository
{
    public interface IPedidosRepository
    {
        Task<List<PedidoEntity>> GetAll();
        Task<PedidoEntity> GetByDroneID(int droneID);
        Task Inserir(PedidoEntity pedido);
        Task Atualizar(PedidoEntity pedido);
        Task Incluir(PedidoEntity pedido);
    }
}
