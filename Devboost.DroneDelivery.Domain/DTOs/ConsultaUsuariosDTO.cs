using Devboost.DroneDelivery.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Devboost.DroneDelivery.Domain.DTOs
{
    public class ConsultaUsuariosDTO
    {        
        public string Situacao { get; set; }
        public List<PedidoEntity> Pedidos { get; set; }        
    }
}