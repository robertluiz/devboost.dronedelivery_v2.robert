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

                return ConvertListModelToModelEntity(list.AsList());
			}
		}

        public async Task<PedidoEntity> GetByDroneID(int droneID)
        {
            using (SqlConnection conexao = new SqlConnection(
                _configuracoes.GetConnectionString(_configConnectionString)))
            {
                var p = await conexao.QuerySingleAsync<Pedido>(
                    @"SELECT *
                    FROM dbo.Pedido
                    WHERE DroneId = @droneID
                    AND Status = 'PendenteEntrega' ",
                    new { Nome = droneID }
                );

                return ConvertModelToModelEntity(p);
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

        public void Atualizar(PedidoEntity pedido)
        {
            using (SqlConnection conexao = new SqlConnection(
                _configuracoes.GetConnectionString(_configConnectionString)))
            {
                var dataAtualizacao = DateTime.Now;

                var query = @"UPDATE Dbo.Pedido		
			        SET Status = @status, DataHora = @DataHora
                    WHERE Id = @id
                ";

                conexao.Execute(query, new { pedido.Status, pedido.DataHora }
              );
            }
        }

        public void Incluir(PedidoEntity pedido)
        {
            using (SqlConnection conexao = new SqlConnection(
                _configuracoes.GetConnectionString(_configConnectionString)))
            {
                conexao.InsertAsync<PedidoEntity>(pedido);
            }
        }


        protected List<PedidoEntity> ConvertListModelToModelEntity(List<Pedido> listPedido)
        {

            List<PedidoEntity> newListD = new List<PedidoEntity>();

            foreach (var item in listPedido)
            {
                newListD.Add(ConvertModelToModelEntity(item));
            }
            return newListD;

        }

        protected PedidoEntity ConvertModelToModelEntity(Pedido pedido)
        {

            PedidoEntity p = new PedidoEntity()
            {
                Id = pedido.Id,
                Status = (PedidoStatus)pedido.Status,
                DroneId = pedido.DroneId,
                DataHora = pedido.DataHora,
                Latitude = pedido.Latitude,
                Longitude = pedido.Longitude,
                PesoGramas = pedido.Peso
            };
            
            return p;

        }

    }
}
