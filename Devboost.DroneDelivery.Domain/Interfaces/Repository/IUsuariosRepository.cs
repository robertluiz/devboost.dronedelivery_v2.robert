using System.Threading.Tasks;
using Devboost.DroneDelivery.Domain.Entities;

namespace Devboost.DroneDelivery.Domain.Interfaces.Repository
{
    public interface IUsuariosRepository
    {

        Task<UsuarioEntity> GetUsuarioByLoginSenha(UsuarioEntity usuario);
    }
}