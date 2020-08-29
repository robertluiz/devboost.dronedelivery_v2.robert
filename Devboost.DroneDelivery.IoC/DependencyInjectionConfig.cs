using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using Devboost.DroneDelivery.Domain.Interfaces.Services;
using Devboost.DroneDelivery.Domain.VOs;
using Devboost.DroneDelivery.DomainService;
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
            services.AddScoped<IDroneService, DroneService>();
            services.AddScoped<IPedidoService, PedidoService>();
            services.AddScoped<IEntregaService, EntregaService>();
            services.AddScoped<IUsuariosRepository, UsuariosRepository>();
            services.AddScoped<IAuthService, AuthService>();
            
            
            
            return services;
        }
    }
}