using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Devboost.DroneDelivery.Domain.Entities;

namespace Devboost.DroneDelivery.Domain.Interfaces.Repository
{
    public interface IUsuariosRepository
    {

        Task Inserir(UsuarioEntity user);
        Task<List<UsuarioEntity>> GetAll();
        Task<UsuarioEntity> GetSingleById(Guid id);
        Task<UsuarioEntity> GetSingleByLogin(string login);        
        Task<UsuarioEntity> GetUsuarioByLoginSenha(UsuarioEntity usuario);
    }
}