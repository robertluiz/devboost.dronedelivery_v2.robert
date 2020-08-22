using System;
using Devboost.DroneDelivery.Domain.Enums;

namespace Devboost.DroneDelivery.Domain.Entities
{
    public class DroneEntity
    {
        public int Id { get; set; }
        public int CapacidadeGamas { get; set; }
        public int VelocidadeKmH { get; set; }
        public int AutonomiaMinitos { get; set; }
        public int CargaGramas { get; set; }
        public DroneStatus Status { get; set; }
        public DateTime DataAtualizacao { get; set; }

    }
}