using System;
using Devboost.DroneDelivery.Domain.Enums;

namespace Devboost.DroneDelivery.Domain.Entities
{
    public class PedidoEntity
    {
        public Guid Id { get; set; }
        public int PesoGramas {get; set;}
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime DataHora { get; set; }
        public PedidoStatus Status { get; set; }
        public DroneEntity Drone { get; set; }
        public readonly int DistanciaMaxima = 17;
        public readonly int PesoGamasMaximo = 12000;

        public bool ValidaPedido(int distanciaKm)
        {
            return distanciaKm <= DistanciaMaxima && PesoGramas <= PesoGamasMaximo;
        }
    }
}