using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Interfaces.Queries;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Devboost.DroneDelivery.DomainService.Queries
{
    public class UsuarioQuery : IUsuarioQuery
    {

        private readonly IUsuariosRepository _usuariosRepository;
        
        public UsuarioQuery(IUsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }

        public async Task<UsuarioEntity> GetSingleByLogin(string login)
        {
            return await _usuariosRepository.GetSingleByLogin(login);
        }

        public async Task<List<UsuarioEntity>> GetAll()
        {
            return await _usuariosRepository.GetAll();
        }
    }
}