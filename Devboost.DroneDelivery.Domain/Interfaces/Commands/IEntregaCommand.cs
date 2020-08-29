using System.Threading.Tasks;

namespace Devboost.DroneDelivery.Domain.Interfaces.Commands
{
    public interface IEntregaCommand
    {
        Task Inicia();
    }
}