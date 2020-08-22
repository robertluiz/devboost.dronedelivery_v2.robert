using Devboost.DroneDelivery.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Devboost.DroneDelivery.Repository.Context
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            /*
            * Por Default o DotNet cria as tabelas no plural, 
            * então usando essa convenção você faz com que a tabela seja criada exatamente com a nomenclatura da sua classe
            */
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Pedido> pedidos { get; set; }

        public DbSet<Drone> drones { get; set; }

    }
}
