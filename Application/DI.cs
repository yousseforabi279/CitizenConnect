using Application.Common.Behaviors;
using Application.Contracts.Repos;
using Application.Core.Commands.CreateComplaint;
using Application.Core.Commands.CreateCompliant;
using Application.Core.Commands.CreateCompliant.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DI).Assembly;
            // MediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);
            });
            // FluentValidation
            services.AddValidatorsFromAssembly(assembly);
            // Validation Behavior
            services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));

            // AutoMapper
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(CreateComplaintMappingProfile).Assembly);
            });
            services.AddScoped<INationalIdValidator, EgyptianNationalIdValidator>();

            return services;

        }
    }
}
