using System;

namespace Devboost.DroneDelivery.Domain.DTOs
{
    public class ConsultaDronePedidoDTO
    {
        public int IdDrone { get; set; }
        public string Situacao { get; set; }
        public Guid? PedidoId { get; set; }
    }
}