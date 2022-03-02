using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using VirtualAccountAutomation.Core.Interfaces;
using VirtualAccountAutomation.Infrastructure.Helpers;
using VirtualAccountAutomation.Infrastructure.Interfaces;
using VirtualAccountAutomation.Infrastructure.Repository;
using VirtualAccountAutomation.Infrastructure.Validators;

namespace VirtualAccountAutomation.API.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            //services.AddControllers()
            //    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PetDto>());
            
            
            
            services.AddScoped<IVirtualAccountRepository, VirtualAccountRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            services.AddFluentValidation(x =>
            {
                x.RegisterValidatorsFromAssemblyContaining<VirtualAccountValidator>();
            });
            
        }
    }
}