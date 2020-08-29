using Devboost.DroneDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Devboost.DroneDelivery.Domain.DTOs
{
    public class ConsultaDronePedidoDTO
    {
        public Guid IdDrone { get; set; }
        public string Situacao { get; set; }

        [JsonIgnore] //Faz com que essa propriedade não seja serializada no JSON
        public List<PedidoEntity> Pedidos { get; set; }
        public List<ConsultaPedidoCompradorDTO> PedidosComprador { get; set; }
    }
}