using FluentValidation;
using JSL.CadCaminhoneiro.Api.Contracts;
using JSL.CadCaminhoneiro.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JSL.CadCaminhoneiro.Api.Infrastructure.Installers
{
    internal class RegisterModelValidators : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IValidator<MotoristaIncluirRequest>, MotoristaIncluirRequestValidator>();
            services.AddTransient<IValidator<MotoristaAlterarRequest>, MotoristaAlterarRequestValidator>();
            
            services.AddTransient<IValidator<MarcaCaminhaoIncluirRequest>, MarcaCaminhaoIncluirRequestValidator>();
            services.AddTransient<IValidator<MarcaCaminhaoAlterarRequest>, MarcaCaminhaoAlterarRequestValidator>();

            services.AddTransient<IValidator<ModeloCaminhaoIncluirRequest>, ModeloCaminhaoIncluirRequestValidator>();
            services.AddTransient<IValidator<ModeloCaminhaoAlterarRequest>, ModeloCaminhaoAlterarRequestValidator>();

            services.AddTransient<IValidator<MotoristaIncluirRequest>, MotoristaIncluirRequestValidator>();
            services.AddTransient<IValidator<MotoristaAlterarRequest>, MotoristaAlterarRequestValidator>();

            //Disable Automatic Model State Validation built-in to ASP.NET Core
            services.Configure<ApiBehaviorOptions>(opt => { opt.SuppressModelStateInvalidFilter = true; });
        }
    }
}
