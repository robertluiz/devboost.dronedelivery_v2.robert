using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Enums;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using Devboost.DroneDelivery.Repository.Models;
using Microsoft.Extensions.Configuration;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Devboost.DroneDelivery.Repository.Implementation
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly string _configConnectionString = "DroneDelivery";
        private readonly IDbConnectionFactoryExtended _dbFactory; 
        
        public UsuariosRepository(IConfiguration config)
        {
            _dbFactory = new OrmLiteConnectionFactory(
                config.GetConnectionString(_configConnectionString),  
                SqlServerDialect.Provider);
        }

        public async Task Inserir(UsuarioEntity user)
        {
            var model = user.ConvertTo<Usuario>();
            using var conexao = await _dbFactory.OpenAsync();

            conexao.CreateTableIfNotExists<Usuario>();
            await conexao.InsertAsync(model);

        }

        public async Task<List<UsuarioEntity>> GetAll()
        {
            using var conexao = await _dbFactory.OpenAsync();

            var list = await conexao.SelectAsync<Usuario>();

            return list.ConvertTo<List<UsuarioEntity>>();

        }

        public async Task<UsuarioEntity> GetSingleById(Guid id)
        {
            using var conexao = await _dbFactory.OpenAsync();
            conexao.CreateTableIfNotExists<Usuario>();
            var u = await conexao.SingleAsync<Usuario>(
                u =>
                    u.Id == id);

            return u.ConvertTo<UsuarioEntity>();

        }

        public async Task<UsuarioEntity> GetSingleByLogin(string login)
        {
            using var conexao = await _dbFactory.OpenAsync();
            conexao.CreateTableIfNotExists<Usuario>();
            var u = await conexao.SingleAsync<Usuario>(
                u =>
                    u.Login.ToLower() == login.ToLower());

            return u.ConvertTo<UsuarioEntity>();

        }

        public async Task<UsuarioEntity> GetUsuarioByLoginSenha(UsuarioEntity usuario)
        {
            using var conexao = await _dbFactory.OpenAsync();
            if (conexao.CreateTableIfNotExists<Usuario>())
            {
                await conexao.InsertAllAsync(SeedUsuario());
            }
            var retornoUsuario = await conexao.SingleAsync<Usuario>(
                u =>
                    u.Login.ToLower() == usuario.Login.ToLower() && u.Senha == usuario.Senha);

            return retornoUsuario.ConvertTo<UsuarioEntity>();
        }

        private static List<Usuario> SeedUsuario()
        {
            return new List<Usuario>
            {
                new Usuario
                {
                    Id = Guid.NewGuid(),
                    Login = "fulano",
                    Nome = "Fulano da Silva Ramos",
                    Role = RoleEnum.Comprador,
                    Senha = "123456#",
                    Latitude = -23.592806,
                    Longitude = -46.674925,
                    DataCadastro = DateTime.Now
                },
                new Usuario
                {
                    Id = Guid.NewGuid(),
                    Login = "ciclano",
                    Nome = "Ciclano da Silva",
                    Role = RoleEnum.Administrador,
                    Senha = "123456#",
                    Latitude = -23.592806,
                    Longitude = -46.674925,
                    DataCadastro = DateTime.Now
                },
            };
        }
    }
}