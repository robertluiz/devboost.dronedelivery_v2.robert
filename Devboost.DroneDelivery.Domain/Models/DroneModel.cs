using System;

namespace Devboost.DroneDelivery.Domain.Models
{
    public class DroneModel
    {
        public Guid Id { get; set; }
        public int Capacidade { get; set; }
        public int Velocidade { get; set; }
        public int Autonomia { get; set; }
        public int Status { get; set; }
        public int Carga { get; set; }
    }
}