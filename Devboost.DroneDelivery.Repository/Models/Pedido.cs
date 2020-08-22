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

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Status { get; set; }
        public DateTime? DataHora { get; set; }
        public int DroneId { get; set; }
    }
}