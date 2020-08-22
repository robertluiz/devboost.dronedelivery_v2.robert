using System;

namespace Devboost.DroneDelivery.Domain.Entities
{
    public class DroneEntity
    {
        public Guid Id { get; set; }
        public int Capacidade { get; set; }
        public int Velocidade { get; set; }
        public int AutonomiaKm { get; set; }
        public int CargaGramas { get; set; }
    }
}