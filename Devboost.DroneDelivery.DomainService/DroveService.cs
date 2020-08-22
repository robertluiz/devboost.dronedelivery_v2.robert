using System;
using System.Collections.Generic;
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

        public DroveService(IDronesRepository dronesRepository)
        {
            _dronesRepository = dronesRepository;
        }

        public async Task<List<ConsultaDronePedidoDTO>> ConsultaDrone()
        {
           // var ListaDrones = await _dronesRepository.getAll();
           AtualizaStatusDrones(ListaDrones);
           
           //return await _dronesRepository.getAll();
        }

        public async Task<DroneEntity> SelecionarDrone()
        {
            //var ListaDrones = await _dronesRepository.getAll();
            var ListaDrones = new List<DroneEntity>();
            AtualizaStatusDrones(ListaDrones);
            return ListaDrones[0];
        }

        public async Task AtualizaDrone(DroneEntity drone)
        {
            //await _dronesRepository.Atualizar(drone);
        }

        private void AtualizaStatusDrones(List<DroneEntity> lista)
        {
            lista.ForEach(AtualizaStatusDrones);
        }
        private void AtualizaStatusDrones(DroneEntity drone)
        {
            drone.DataAtualizacao ??= DateTime.Now;
            var total = (drone.DataAtualizacao - DateTime.Now).Value.TotalMinutes;
            
            switch (drone.Status)
            {
                case DroneStatus.Pronto:
                    break;
                case DroneStatus.EmTransito:
                    
                    if (total > drone.AUTONOMIA_RECARGA)
                    {
                        drone.Status = DroneStatus.Pronto;
                        drone.DataAtualizacao = DateTime.Now;
                       // _dronesRepository.Atualizar(drone);
                    }

                    if (total > drone.AUTONOMIA_MAXIMA)
                    {
                        drone.Status = DroneStatus.Carregando;
                        drone.DataAtualizacao = DateTime.Now;
                        // _dronesRepository.Atualizar(drone);
                    }
                    
                    break;
                case DroneStatus.Carregando:
                    if (total > drone.TEMPO_RECARGA_MINUTOS)
                    {
                        drone.Status = DroneStatus.Pronto;
                        drone.DataAtualizacao = DateTime.Now;
                        // _dronesRepository.Atualizar(drone);
                    }
                    break;
                default:
                    drone.Status = DroneStatus.Pronto;
                    drone.DataAtualizacao = DateTime.Now;
                    // _dronesRepository.AtualizaR(drone);
                    break;
            }
        }
    }
}