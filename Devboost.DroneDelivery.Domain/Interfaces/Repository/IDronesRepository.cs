using Devboost.DroneDelivery.Domain.Entities;
using System.Collections.Generic;

namespace Devboost.DroneDelivery.Domain.Interfaces.Repository
{
    public interface IDronesRepository
    {
        List<DroneEntity> GetAll();
        List<DroneEntity> GetByStatus(string status);
        void Atualizar(DroneEntity drone);

    }
}
