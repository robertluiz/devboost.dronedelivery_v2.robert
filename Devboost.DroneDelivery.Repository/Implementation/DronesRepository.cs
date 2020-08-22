using Dapper;
using Dapper.Contrib.Extensions;
using Devboost.DroneDelivery.Repository.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Devboost.DroneDelivery.Repository.Implementation
{
    public class DronesRepository
    {

		protected readonly string _configConnectionString = "DroneDelivery";

		private IConfiguration _configuracoes;
		public DronesRepository(IConfiguration config)
		{
			_configuracoes = config;
		}

		public List<Drone> GetAll()
		{
			using (SqlConnection conexao = new SqlConnection(
				_configuracoes.GetConnectionString(_configConnectionString)))
			{
				var list = conexao.GetAll<Drone>();
				return list.AsList();
			}
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
