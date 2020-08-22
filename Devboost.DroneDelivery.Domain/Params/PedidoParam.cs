using System;
using System.ComponentModel.DataAnnotations;
namespace Devboost.DroneDelivery.Domain.Params
{
    public class PedidoParam
    {
        [Required]
        public int Peso { get; set; }
        [Required]
        public string Latitude { get; set; }
        [Required]
        public string Longitude { get; set; }
        [Required]
        public DateTime DataHora { get; set; }
    }
}