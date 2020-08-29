using System;
using System.ComponentModel.DataAnnotations.Schema;
using Devboost.DroneDelivery.Domain.Enums;

namespace Devboost.DroneDelivery.Repository.Models
{
    [Table("dbo.Usuario")]
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public RoleEnum Role { get; set; }        
        public double Latitude { get; set; }        
        public double Longitude { get; set; }        
        public DateTime DataCadastro { get; set; }
    }
}