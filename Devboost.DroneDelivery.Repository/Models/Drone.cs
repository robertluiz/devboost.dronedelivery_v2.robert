using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Devboost.DroneDelivery.Repository.Models
{
    [Table("dbo.Drone")]
    public class Drone
    {
        public Drone()
        {
            Id = Guid.NewGuid();
            DataAtualizacao = DateTime.Now;
        }

        public Guid Id { get; set; }
        public int Capacidade { get; set; }
        public int Velocidade { get; set; }
        public int Autonomia { get; set; }
        public string Status { get; set; }
        public int Carga { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}