using JSL.CadCaminhoneiro.Api.Contracts;
using JSL.CadCaminhoneiro.Data.Repository;
using JSL.CadCaminhoneiro.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JSL.CadCaminhoneiro.Api.Infrastructure.Installers
{
    internal class RegisterRepositories : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IMotoristaRepository, MotoristaRepository>();
            services.AddTransient<ICaminhaoRepository, CaminhaoRepository>();
            services.AddTransient<IMarcaCaminhaoRepository, MarcaCaminhaoRepository>();
            services.AddTransient<IModeloCaminhaoRepository, ModeloCaminhaoRepository>();
        }
    }
}
