﻿using Devboost.DroneDelivery.Domain.Interfaces.Commands;
using Devboost.DroneDelivery.Domain.Interfaces.Queries;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using Devboost.DroneDelivery.Domain.Interfaces.Services;
using Devboost.DroneDelivery.DomainService;
using Devboost.DroneDelivery.DomainService.Commands;
using Devboost.DroneDelivery.DomainService.Queries;
using Devboost.DroneDelivery.Repository.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devboost.DroneDelivery.IoC
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services,IConfiguration config )
        {
            services.AddSingleton(config);
            services.AddScoped<IDronesRepository, DronesRepository>(); 
            services.AddScoped<IPedidosRepository, PedidosRepository>();            
            services.AddScoped<IUsuariosRepository, UsuariosRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEntregaCommand, EntregaCommand>();
            services.AddScoped<IDroneCommand, DroneCommand>();
            services.AddScoped<IDroneQuery, DroneQuery>();
            services.AddScoped<IPedidoCommand, PedidoCommand>();
            services.AddScoped<IPedidoQuery, PedidoQuery>();
            services.AddScoped<IUsuarioCommand, UsuarioCommand>();
            services.AddScoped<IUsuarioQuery, UsuarioQuery>();

            return services;
        }
    }
}