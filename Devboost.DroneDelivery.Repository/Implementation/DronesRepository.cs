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
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Devboost.DroneDelivery.Repository.Implementation
{
    public class DronesRepository : IDronesRepository
    {

		protected readonly string _configConnectionString = "DroneDelivery";

		private IConfiguration _configuracoes;
		public DronesRepository(IConfiguration config)
		{
			_configuracoes = config;
		}

		public async Task<List<DroneEntity>> GetAll()
		{
			using (SqlConnection conexao = new SqlConnection(
				_configuracoes.GetConnectionString(_configConnectionString)))
			{
				var list = await conexao.GetAllAsync<Drone>();
                
                return ConvertModelToModelEntity(list.AsList());
			}
		}

        public async Task<List<DroneEntity>> GetByStatus(string status)
        {
            using (SqlConnection conexao = new SqlConnection(
                _configuracoes.GetConnectionString(_configConnectionString)))
            {
                var list = await conexao.QueryAsync<Drone>(
                    "SELECT * " +
                    "FROM dbo.Drone " +
                    "WHERE Status = @Status",
                    new { Nome = status }
                );

                return ConvertModelToModelEntity(list.AsList());
            }
        }

            public void Atualizar(DroneEntity drone)
        {
            using (SqlConnection conexao = new SqlConnection(
                _configuracoes.GetConnectionString(_configConnectionString)))
            {
                var dataAtualizacao = DateTime.Now;

                var query = @"UPDATE Dbo.Drone		
			        SET Status = @status, DataAtualizacao = @dataAtualizacao
                    WHERE Id = @id
                ";

                conexao.Execute(query, new { drone.Status, dataAtualizacao, drone.Id }
              );
            }
        }

        protected List<DroneEntity> ConvertModelToModelEntity(List<Drone> listDrone)        {

            List<DroneEntity> newListD = new List<DroneEntity>();

            foreach (var item in listDrone)
            {
                DroneEntity d = new DroneEntity()
                {
                    Id = item.Id,
                    Status = (DroneStatus)item.Status,
                    AutonomiaMinitos = item.Autonomia,
                    CapacidadeGamas = item.Capacidade,
                    VelocidadeKmH = item.Velocidade,
                    DataAtualizacao = item.DataAtualizacao
                };

                newListD.Add(d);
            }
            return newListD;

        }
        

        //public IEnumerable<DroneModel> ObterTodos()
        //{
        //	using (SqlConnection conexao = new SqlConnection(
        //		_configuracoes.GetConnectionString(_configConnectionString)))
        //	{

        //		var query = @"SELECT *
        //	FROM Dbo.Jogador J
        //	INNER JOIN Dbo.Posicao P ON J.IDPosicao = P.IDPosicao
        //	INNER JOIN Dbo.Clube C ON J.IDClube = C.IDClube";

        //		return conexao.Query<Jogador, Posicao, Clube, Jogador>(
        //			  query,
        //			  map: (jogador, posicao, clube) => {
        //				  jogador.Posicao = posicao;
        //				  jogador.Clube = clube;
        //				  ;
        //				  return jogador;
        //			  },
        //			  splitOn: "CPF,IDPosicao, IDClube"
        //		);
        //	}
        //}

        //public void Inserir(Jogador jogador)
        //{
        //	using (SqlConnection conexao = new SqlConnection(
        //		_configuracoes.GetConnectionString(_configConnectionString)))
        //	{

        //		var query = @"INSERT INTO Dbo.Jogador		
        //	VALUES(
        //	@CPF,
        //	@Nome,
        //	@IDPosicao,
        //	@IDClube
        //	)";

        //		conexao.Execute(query, new { jogador.CPF, jogador.Nome, jogador.Posicao.IDPosicao, jogador.Clube.IDClube }
        //	  );
        //	}
        //}

        //public Jogador ObterByName(string name)
        //{
        //	using (SqlConnection conexao = new SqlConnection(
        //		_configuracoes.GetConnectionString(_configConnectionString)))
        //	{
        //		return conexao.QueryFirstOrDefault<Jogador>(
        //			"SELECT * " +
        //			"FROM dbo.Jogadores " +
        //			"WHERE Nome = @Nome ",
        //			new { Nome = name }
        //		);
        //	}
        //}
    }
}
