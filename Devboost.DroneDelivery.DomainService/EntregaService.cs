using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Enums;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using Devboost.DroneDelivery.Domain.Interfaces.Services;
using Devboost.DroneDelivery.Domain.Params;

namespace Devboost.DroneDelivery.DomainService
{
    public class EntregaService : IEntregaService
    {
        private readonly IDroneService _droneService;        

        public EntregaService(IDroneService droneService)
        {
            _droneService = droneService;            
        }

        public async Task Inicia()
        {
            _droneService.LiberaDrone();
        }        
    }
}