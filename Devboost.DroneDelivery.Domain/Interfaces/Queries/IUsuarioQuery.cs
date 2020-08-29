using Devboost.DroneDelivery.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Devboost.DroneDelivery.Domain.Interfaces.Queries
{
    public interface IUsuarioQuery
    {
        Task<UsuarioEntity> GetSingleByLogin(string login);
        Task<List<UsuarioEntity>> GetAll();        
    }
}