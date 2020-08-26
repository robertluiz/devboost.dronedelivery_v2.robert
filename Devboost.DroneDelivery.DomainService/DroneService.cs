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
    public class DroneService : IDroneService
    {
        private readonly IDronesRepository _dronesRepository;
        private readonly IPedidosRepository _pedidosRepository;
        public DroneService(IDronesRepository dronesRepository, IPedidosRepository pedidosRepository)
        {
            _dronesRepository = dronesRepository;
            _pedidosRepository = pedidosRepository;
        }

        public async Task<List<ConsultaDronePedidoDTO>> ConsultaDrone()
        {
            var listaDrones = await _dronesRepository.GetAll();
            AtualizaStatusDrones(listaDrones);
            var drones = await _dronesRepository.GetAll();
            return drones.Select(async d => await RetornConsultaDronePedido(d))
                .ToList()
                .Select(c => c.Result)
                .ToList();

        }

        private async Task<ConsultaDronePedidoDTO> RetornConsultaDronePedido(DroneEntity drone)
        {

            var pedidos = await _pedidosRepository.GetByDroneID(drone.Id);

            return new ConsultaDronePedidoDTO
            {

                IdDrone = drone.Id,
                Situacao = drone.Status.ToString(),
                Pedidos = pedidos
            };
        }

        public async Task<DroneEntity> SelecionarDrone(PedidoEntity pedido)
        {
            DroneEntity drone = null;

            var listaDrones = await _dronesRepository.GetAll();

            foreach (var item in listaDrones)
            {
                await AtualizaStatusDrones(item); //Aproveito para atualizar o status de cada drone

                if (!Disponivel(item)) //Se o drone não encontra-se disponível, então pula para verificar a disponibilidade do próximo
                    continue;

                var listPedidosDrone = await RetornConsultaDronePedido(item);

                if (SuportaPeso(listPedidosDrone, item, pedido) && TemAutonomiaSuficiente(listPedidosDrone, item, pedido)) 
                {
                    drone = item;
                    break;
                }
            }
            return drone;
        }

        public async Task LiberaDrone()
        {

            var listaDrones = await _dronesRepository.GetByStatus(DroneStatus.Pronto.ToString());

            foreach (var item in listaDrones)
            {
                var listPedidosDrone = await RetornConsultaDronePedido(item);

                if (!TemPedido(listPedidosDrone)) //Se o drone ainda não tem pedido, então ele não será atualizado o seu status, pois não tem entrega para efetuar
                {
                    continue;
                }

                if (listPedidosDrone != null)
                {
                    foreach (var p in listPedidosDrone.Pedidos)
                    {
                        p.Status = PedidoStatus.EmTransito.ToString();
                        await _pedidosRepository.Atualizar(p);
                    }
                }

                item.Status = DroneStatus.EmTransito;
                item.DataAtualizacao = DateTime.Now;
                await AtualizaDrone(item);
            }
        }


        public async Task AtualizaDrone(DroneEntity drone)
        {
            if (drone == null)
                return;

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
                    var pedido = await _pedidosRepository.GetSingleByDroneID(drone.Id);
                    if (total > drone.AUTONOMIA_RECARGA)
                    {
                        drone.Status = DroneStatus.Pronto;
                        drone.DataAtualizacao = DateTime.Now;
                        await _dronesRepository.Atualizar(drone);
                        pedido.Status = PedidoStatus.Entregue.ToString();
                        await _pedidosRepository.Atualizar(pedido);
                    }

                    if (total > drone.AUTONOMIA_MAXIMA)
                    {
                        drone.Status = DroneStatus.Carregando;
                        drone.DataAtualizacao = DateTime.Now;
                        await _dronesRepository.Atualizar(drone);
                        pedido.Status = PedidoStatus.Entregue.ToString();
                        await _pedidosRepository.Atualizar(pedido);
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

        #region methods private
        private bool Disponivel(DroneEntity drone)
        {
            return drone.Status.Equals(DroneStatus.Pronto);
        }

        private bool TemPedido(ConsultaDronePedidoDTO listPedidosDrone)
        {
            if (listPedidosDrone.Pedidos == null || listPedidosDrone.Pedidos.Count().Equals(0)) //Se o drone não tem pedidos relacionados, então já pego ele e saio do foreach
            {
                return false;
            }
            return true;
        }

        private bool SuportaPeso(ConsultaDronePedidoDTO listDronePedidos, DroneEntity drone, PedidoEntity pedidoNovo)
        {
            bool suporta = false;

            var pesoDeTodosPedidos = listDronePedidos != null && listDronePedidos.Pedidos != null && listDronePedidos.Pedidos.Count > 0 ? (listDronePedidos.Pedidos.Sum(s => s.Peso) + pedidoNovo.Peso) : pedidoNovo.Peso;

            if (pesoDeTodosPedidos <= drone.Capacidade)
            {
                suporta = true;
            }

            return suporta;
        }

        private bool TemAutonomiaSuficiente(ConsultaDronePedidoDTO listDronePedidos, DroneEntity drone, PedidoEntity pedidoNovo)
        {
            bool temAuto = false;

            var distanciaDeTodosPedidosEmMetros = listDronePedidos != null && listDronePedidos.Pedidos != null && listDronePedidos.Pedidos.Count > 0 ? (listDronePedidos.Pedidos.Sum(s => s.DistanciaDaEntrega) + pedidoNovo.DistanciaDaEntrega) : pedidoNovo.DistanciaDaEntrega;
            var distanciaDeTodosPedidosKM = distanciaDeTodosPedidosEmMetros / 1000;

            var tempoTotalDeViagemParaTodosPedidosEmMinutos = (distanciaDeTodosPedidosKM * 60) / drone.Velocidade; //Multiplicando o KM por 60 minutos para representar Km/h e depois divido pela velocidade máxima do drone
            
            if ((tempoTotalDeViagemParaTodosPedidosEmMinutos * 2) <= drone.Autonomia) // Multiplico por 2 porque tem a ida e a volta, assim saberei o tempo total gasto em Minutos para entregar os pedidos
            {
                temAuto = true;
            }

            return temAuto;

        }
        #endregion
    }
}