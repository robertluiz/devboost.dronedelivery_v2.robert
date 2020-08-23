using System;
using System.Collections.Generic;
using System.Linq;
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
    public class DronesRepository : IDronesRepository
    {
        private readonly string _configConnectionString = "DroneDelivery";
        private readonly IDbConnectionFactoryExtended _dbFactory; 
        
        public DronesRepository(IConfiguration config)
        {
            _dbFactory = new OrmLiteConnectionFactory(
                config.GetConnectionString(_configConnectionString),  
                SqlServerDialect.Provider);
        }

        public async Task<List<DroneEntity>> GetAll()
        {
            using var conexao= await _dbFactory.OpenAsync();
            conexao.CreateTableIfNotExists<Drone>();
            var list = await conexao.SelectAsync<Drone>();
                
            return list.ConvertTo<List<DroneEntity>>();
        }

        public async Task<List<DroneEntity>> GetByStatus(string status)
        {
            using var conexao = await _dbFactory.OpenAsync();
            conexao.CreateTableIfNotExists<Drone>();
            
            var list = await conexao.SelectAsync<Drone>(d => d.Status == status);
            return list.ConvertTo<List<DroneEntity>>();
        }

        public async Task Atualizar(DroneEntity drone)
        {
            var model = drone.ConvertTo<Drone>();
            using var conexao = await _dbFactory.OpenAsync();
            
            conexao.CreateTableIfNotExists<Drone>();
            await conexao.UpdateAsync(model);
            
        }

        public async Task Incluir(DroneEntity drone)
        {
            var model = drone.ConvertTo<Drone>();
            using var conexao=  await _dbFactory.OpenAsync();
            
            conexao.CreateTableIfNotExists<Drone>();
            await conexao.InsertAsync(model);
        }

        private static List<DroneEntity> ConvertListModelToModelEntity(List<Drone> listDrone)
        {
            return listDrone.Select(ConvertModelToModelEntity).ToList();
        }

        private static DroneEntity ConvertModelToModelEntity(Drone drone)
        {

            var p = new DroneEntity()
            {
                Id = drone.Id,
                Status = Enum.Parse<DroneStatus>(drone.Status),
                AutonomiaMinitos = drone.Autonomia,
                CapacidadeGamas = drone.Capacidade,
                VelocidadeKmH = drone.Velocidade,
                DataAtualizacao = drone.DataAtualizacao
            };

            return p;

        }
        
    }
}