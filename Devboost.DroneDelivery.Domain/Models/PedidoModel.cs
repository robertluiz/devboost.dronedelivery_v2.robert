using System;
using Devboost.DroneDelivery.Domain.Entities;

namespace Devboost.DroneDelivery.Domain.Models
{
    public class PedidoModel
    {
        public Guid Id { get; set; }
        public int Peso {get; set;}
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int Status { get; set; }
        public DateTime? DataHora { get; set; }
        public Guid DroneId { get; set; }
    }
}