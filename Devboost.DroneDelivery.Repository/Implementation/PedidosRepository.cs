using Dapper;
using Dapper.Contrib.Extensions;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Enums;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using Devboost.DroneDelivery.Repository.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Devboost.DroneDelivery.Repository.Implementation
{
    public class PedidosRepository : IPedidosRepository
    {

		protected readonly string _configConnectionString = "DroneDelivery";

		private IConfiguration _configuracoes;
		public PedidosRepository(IConfiguration config)
		{
			_configuracoes = config;
		}

		public async Task<List<PedidoEntity>> GetAll()
		{
			using (SqlConnection conexao = new SqlConnection(
				_configuracoes.GetConnectionString(_configConnectionString)))
			{

                var list = await conexao.GetAllAsync<Pedido>();

                return ConvertModelToModelEntity(list.AsList());
			}
		}

        public async Task<List<PedidoEntity>> GetByDroneID(int droneID)
        {
            using (SqlConnection conexao = new SqlConnection(
                _configuracoes.GetConnectionString(_configConnectionString)))
            {
                var list = await conexao.QueryAsync<Pedido>(
                    "SELECT * " +
                    "FROM dbo.Pedido " +
                    "WHERE DroneId = @droneID",
                    new { Nome = droneID }
                );

                return ConvertModelToModelEntity(list.AsList());
            }
        }

        public void Inserir(PedidoEntity pedido)
        {
            using (SqlConnection conexao = new SqlConnection(
                _configuracoes.GetConnectionString(_configConnectionString)))
            {

				var Id = Guid.NewGuid();

				var query = @"INSERT INTO Dbo.Pedido(id, peso, latitude, longitude, datahora, droneId)		
				VALUES(
				@Id,
				@Peso,
				@Latitude,
				@Longitude,
				@DataHora,
				@DroneId
				)";

                conexao.Execute(query, new { Id, pedido.PesoGramas, pedido.Latitude, pedido.Longitude, pedido.DataHora, pedido.DroneId }
              );
            }
        }


        protected List<PedidoEntity> ConvertModelToModelEntity(List<Pedido> listDrone)
        {

            List<PedidoEntity> newListD = new List<PedidoEntity>();

            foreach (var item in listDrone)
            {
                PedidoEntity d = new PedidoEntity()
                {
                    Id = item.Id,
                    Status = (PedidoStatus)item.Status,
                    DroneId = item.DroneId,
                    DataHora = item.DataHora,
                    Latitude = item.Latitude,
                    Longitude = item.Longitude,
                    PesoGramas = item.Peso
                };

                newListD.Add(d);
            }
            return newListD;

        }

    }
}
