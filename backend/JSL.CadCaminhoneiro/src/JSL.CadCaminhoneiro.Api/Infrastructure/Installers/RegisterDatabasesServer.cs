using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using JSL.CadCaminhoneiro.Api.Contracts;
using JSL.CadCaminhoneiro.Data;

namespace JSL.CadCaminhoneiro.Api.Infrastructure.Installers
{
    internal class RegisterDatabasesServer : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CadCaminhoneiroContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SQLDBConnectionString")));            
        }
    }
}
