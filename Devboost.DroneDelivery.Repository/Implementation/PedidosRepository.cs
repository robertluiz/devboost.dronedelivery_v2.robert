using Dapper;
using Dapper.Contrib.Extensions;
using Devboost.DroneDelivery.Repository.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Devboost.DroneDelivery.Repository.Implementation
{
    public class PedidosRepository
    {

		protected readonly string _configConnectionString = "DroneDelivery";

		private IConfiguration _configuracoes;
		public PedidosRepository(IConfiguration config)
		{
			_configuracoes = config;
		}

		public List<Pedido> GetAll()
		{
			using (SqlConnection conexao = new SqlConnection(
				_configuracoes.GetConnectionString(_configConnectionString)))
			{
				var list = conexao.GetAll<Pedido>();
				return list.AsList();
			}
		}

        public void Inserir(Pedido pedido)
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

                conexao.Execute(query, new { Id, pedido.Peso, pedido.Latitude, pedido.Longitude, pedido.DataHora, pedido.DroneId }
              );
            }
        }


    }
}
