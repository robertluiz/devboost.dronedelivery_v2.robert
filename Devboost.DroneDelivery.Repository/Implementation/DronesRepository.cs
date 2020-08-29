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
            if (conexao.CreateTableIfNotExists<Drone>())
            {
                await conexao.InsertAllAsync(SeedDrone());
            }
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
        

        private static List<Drone> SeedDrone()
        {

            return new List<Drone>{
                new Drone
                {
                    Id = Guid.NewGuid(),
                    Status = DroneStatus.Pronto.ToString(),
                    Autonomia = 35, //Minutos
                    Capacidade = 12000, //gramas
                    Velocidade = 60, //Km por h
                    Carga = 60, //Minutos para carregar totalmente
                    DataAtualizacao = DateTime.Now
                },
                new Drone
                {
                    Id = Guid.NewGuid(),
                    Status = DroneStatus.Pronto.ToString(),
                    Autonomia = 35,
                    Capacidade = 12000,
                    Velocidade = 60,
                    Carga = 60,
                    DataAtualizacao = DateTime.Now
                },
                new Drone
                {
                    Id = Guid.NewGuid(),
                    Status = DroneStatus.Pronto.ToString(),
                    Autonomia = 35,
                    Capacidade = 12000,
                    Velocidade = 60,
                    Carga = 60,
                    DataAtualizacao = DateTime.Now
                },
                new Drone
                {
                    Id = Guid.NewGuid(),
                    Status = DroneStatus.Pronto.ToString(),
                    Autonomia = 35,
                    Capacidade = 12000,
                    Velocidade = 60,
                    Carga = 60,
                    DataAtualizacao = DateTime.Now
                },
                new Drone
                {
                    Id = Guid.NewGuid(),
                    Status = DroneStatus.Pronto.ToString(),
                    Autonomia = 35,
                    Capacidade = 12000,
                    Velocidade = 60,
                    Carga = 60,
                    DataAtualizacao = DateTime.Now
                },
                new Drone
                {
                    Id = Guid.NewGuid(),
                    Status = DroneStatus.Pronto.ToString(),
                    Autonomia = 35,
                    Capacidade = 12000,
                    Velocidade = 60,
                    Carga = 60,
                    DataAtualizacao = DateTime.Now
                },
                new Drone
                {
                    Id = Guid.NewGuid(),
                    Status = DroneStatus.Pronto.ToString(),
                    Autonomia = 35,
                    Capacidade = 12000,
                    Velocidade = 60,
                    Carga = 60,
                    DataAtualizacao = DateTime.Now
                },
            };

        }
        
    }
}