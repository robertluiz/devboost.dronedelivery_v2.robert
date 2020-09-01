using System;
using System.ComponentModel.DataAnnotations;
namespace Devboost.DroneDelivery.Domain.Params
{
    public class UsuarioParam
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
    }
}