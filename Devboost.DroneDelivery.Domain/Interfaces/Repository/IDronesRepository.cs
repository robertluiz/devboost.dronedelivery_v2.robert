using Devboost.DroneDelivery.Domain.Entities;
using System.Collections.Generic;

using System.Collections.Generic;
using Devboost.DroneDelivery.Domain.Entities;

namespace Devboost.DroneDelivery.Domain.Interfaces.Repository
{
    public interface IDronesRepository
    {
        List<DroneEntity> GetAll();

    }
}
