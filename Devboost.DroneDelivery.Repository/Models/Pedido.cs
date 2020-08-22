using Dapper.Contrib.Extensions;
using System;

namespace Devboost.DroneDelivery.Repository.Models
{
    [Table("dbo.Pedido")]
    public class Pedido
    {        

        [ExplicitKey]
        public Guid Id { get; set; }
        public int Peso {get; set;}

        //public DbGeography LatLong { get; set; }        

        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int Status { get; set; }
        public DateTime? DataHora { get; set; }
        public Guid DroneId { get; set; }
    }
}