using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Enums;
using Devboost.DroneDelivery.Domain.Interfaces.Commands;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using Devboost.DroneDelivery.Domain.Interfaces.Services;
using Devboost.DroneDelivery.Domain.Params;

namespace Devboost.DroneDelivery.DomainService.Commands
{
    public class PedidoCommand : IPedidoCommand
    {
        private readonly IDroneCommand _droneCommand;
        private readonly IPedidosRepository _pedidosRepository;

        public PedidoCommand(IDroneCommand droneCommand, IPedidosRepository pedidosRepository)
        {
            _droneCommand = droneCommand;
            _pedidosRepository = pedidosRepository;
        }

        public async Task<bool> InserirPedido(PedidoParam pedido)
        {
            var novoPedido = new PedidoEntity
            {
                Id = Guid.NewGuid(),
                Peso = pedido.Peso,
                Latitude = pedido.Latitude,
                Longitude = pedido.Longitude,
                DataHora = pedido.DataHora,
            };

            //calculoDistancia
            novoPedido.DistanciaDaEntrega = GeolocalizacaoService.CalcularDistanciaEmMetro(pedido.Latitude, pedido.Longitude);
            //var distanciaEmMilhas = GeolocalizacaoService.distance(pedido.Latitude, pedido.Longitude, 'M');
            //var distanciaEmMilhasNauticas = GeolocalizacaoService.distance(pedido.Latitude, pedido.Longitude, 'N');

            if (!novoPedido.ValidaPedido())
                return false;

            var drone = await _droneCommand.SelecionarDrone(novoPedido);

            novoPedido.Drone = drone;
            novoPedido.DroneId = drone != null ? drone.Id : novoPedido.DroneId;
            novoPedido.Status = PedidoStatus.PendenteEntrega.ToString();
            await _pedidosRepository.Inserir(novoPedido);
            await _droneCommand.AtualizaDrone(drone);

            return true;
        }

        public async Task<PedidoEntity> PedidoPorIdDrone(Guid droneId)
        {
            return await _pedidosRepository.GetSingleByDroneID(droneId);
        }

        public async Task AtualizaPedido(PedidoEntity pedido)
        {
            await _pedidosRepository.Atualizar(pedido);
        }

        public async Task<List<PedidoEntity>> GetAll()
        {
            return await _pedidosRepository.GetAll();
        }
    }
}