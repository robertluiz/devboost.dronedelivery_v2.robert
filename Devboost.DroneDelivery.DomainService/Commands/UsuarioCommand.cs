using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Enums;
using Devboost.DroneDelivery.Domain.Interfaces.Commands;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using Devboost.DroneDelivery.Domain.Params;
using System;
using System.Threading.Tasks;

namespace Devboost.DroneDelivery.DomainService.Commands
{
    public class UsuarioCommand : IUsuarioCommand
    {
        
        private readonly IUsuariosRepository _usuariosRepository;

        public UsuarioCommand(IUsuariosRepository usuariosRepository)
        {            
            _usuariosRepository = usuariosRepository;
        }        

        public async Task<bool> Criar(UsuarioParam user)
        {

            var hasRole = Enum.TryParse<RoleEnum>(user.Role, true, out RoleEnum roleUser);

            if (!hasRole)
                throw new Exception("Perfil não encontrado!");


            UsuarioEntity u = new UsuarioEntity {  Id = Guid.NewGuid(), Login = user.Login, Senha = user.Senha, Nome = user.Nome, DataCadastro = DateTime.Now, Role = roleUser, Latitude = user.Latitude, Longitude = user.Longitude };

            await _usuariosRepository.Inserir(u);

            return true;
        }
    }
}