using Devboost.DroneDelivery.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Devboost.DroneDelivery.Domain.DTOs
{
    public class ConsultaDronePedidoDTO
    {
        public Guid IdDrone { get; set; }
        public string Situacao { get; set; }
        public List<PedidoEntity> Pedidos { get; set; }        
    }
}