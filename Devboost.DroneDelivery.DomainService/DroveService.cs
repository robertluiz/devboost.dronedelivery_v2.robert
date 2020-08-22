using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Devboost.DroneDelivery.Domain.DTOs;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Enums;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using Devboost.DroneDelivery.Domain.Interfaces.Services;

namespace Devboost.DroneDelivery.DomainService
{
    public class DroveService : IDroneService
    {
        private readonly IDronesRepository _dronesRepository;
        private readonly IPedidoService _pedidoService;
        public DroveService(IDronesRepository dronesRepository)
        {
            _dronesRepository = dronesRepository;
        }

        public async Task<List<ConsultaDronePedidoDTO>> ConsultaDrone()
        {
           var listaDrones = await _dronesRepository.GetAll();
           AtualizaStatusDrones(listaDrones);
           var drones = await _dronesRepository.GetAll();
           return  drones.Select(async d => await RetornConsultaDronePedido(d))
               .ToList()
               .Select(c => c.Result)
               .ToList();

        }

        private async Task<ConsultaDronePedidoDTO> RetornConsultaDronePedido(DroneEntity drone)
        {
            
            var pedido = await _pedidoService.PedidoPorIdDrone(drone.Id);
            var idPedido = pedido == null ? (Guid?) null : pedido.Id;
            return new ConsultaDronePedidoDTO
            {

                IdDrone = drone.Id,
                Situacao = drone.Status.ToString(),
                PedidoId = idPedido
            };
        }
        
        public async Task<DroneEntity> SelecionarDrone()
        {
            var listaDrones = await _dronesRepository.GetAll();

            AtualizaStatusDrones(listaDrones);
            var drones = await _dronesRepository.GetByStatus(DroneStatus.Pronto.ToString());
            return drones.Count > 0 ? drones[0] : null;
        }

        public async Task AtualizaDrone(DroneEntity drone)
        {
          await _dronesRepository.Atualizar(drone);
        }

        private void AtualizaStatusDrones(List<DroneEntity> lista)
        {
            lista.ForEach(async (d) => await AtualizaStatusDrones(d));
        }
        private async Task AtualizaStatusDrones(DroneEntity drone)
        {
            drone.DataAtualizacao ??= DateTime.Now;
            var total = (drone.DataAtualizacao - DateTime.Now).Value.TotalMinutes;
            
            switch (drone.Status)
            {
                case DroneStatus.Pronto:
                    break;
                case DroneStatus.EmTransito:
                    var pedido =  await _pedidoService.PedidoPorIdDrone(drone.Id);
                    if (total > drone.AUTONOMIA_RECARGA)
                    {
                        drone.Status = DroneStatus.Pronto;
                        drone.DataAtualizacao = DateTime.Now;
                        await _dronesRepository.Atualizar(drone);
                        pedido.Status = PedidoStatus.Entregue;
                        await _pedidoService.AtualizaPedido(pedido);
                    }

                    if (total > drone.AUTONOMIA_MAXIMA)
                    {
                        drone.Status = DroneStatus.Carregando;
                        drone.DataAtualizacao = DateTime.Now;
                        await  _dronesRepository.Atualizar(drone);
                        pedido.Status = PedidoStatus.Entregue;
                        await _pedidoService.AtualizaPedido(pedido);
                    }
                    
                    break;
                case DroneStatus.Carregando:
                    if (total > drone.TEMPO_RECARGA_MINUTOS)
                    {
                        drone.Status = DroneStatus.Pronto;
                        drone.DataAtualizacao = DateTime.Now;
                        await _dronesRepository.Atualizar(drone);
                    }
                    break;
                default:
                    drone.Status = DroneStatus.Pronto;
                    drone.DataAtualizacao = DateTime.Now;
                    await _dronesRepository.Atualizar(drone);
                    break;
            }
        }
    }
}