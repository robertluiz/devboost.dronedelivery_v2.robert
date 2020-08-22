using System;
using System.Threading.Tasks;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Interfaces.Services;
using Devboost.DroneDelivery.Domain.Params;

namespace Devboost.DroneDelivery.DomainService
{
    public class PedidoService: IPedidoService
    {
        public async Task<bool> InserirPedido(PedidoParam pedido)
        {
            var novoPedido = new PedidoEntity
            {
                PesoGramas = pedido.Peso,
                Latitude = pedido.Latitude,
                Longitude = pedido.Longitude,
                DataHora = pedido.DataHora,
                };
            
            //calculoDistancia

            var distancia = GeolocalizacaoService.CalcularDistanciaEmMetro(pedido.Latitude,pedido.Longitude);
            
            if (!novoPedido.ValidaPedido(distancia))
            {
                return await Task.Factory.StartNew(()=> false);
            }
            
            
            
            return true;
        }
    }
}