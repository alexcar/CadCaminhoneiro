using JSL.CadCaminhoneiro.Api.Contracts;
using JSL.CadCaminhoneiro.Api.Infrastructure.Notifications;
using JSL.CadCaminhoneiro.Api.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JSL.CadCaminhoneiro.Api.Infrastructure.Installers
{
    internal class RegisterServices : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IMotoristaService, MotoristaService>();
            services.AddTransient<IMarcaCaminhaoService, MarcaCaminhaoService>();
            services.AddTransient<NotificationContext>();
        }
    }
}
