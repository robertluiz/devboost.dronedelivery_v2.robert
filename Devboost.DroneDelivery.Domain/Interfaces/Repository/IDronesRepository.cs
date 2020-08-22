using Devboost.DroneDelivery.Domain.Entities;
using System.Collections.Generic;

using System.Collections.Generic;
using Devboost.DroneDelivery.Domain.Entities;

namespace Devboost.DroneDelivery.Domain.Interfaces.Repository
{
    public interface IDronesRepository
    {
        List<DroneEntity> GetAll();
        List<DroneEntity> GetByStatus(string status);
        void Atualizar(DroneEntity drone);

    }
}
