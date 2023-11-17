using FluentValidation.AspNetCore;
using FluentValidation;
using Module_02.Task_01.CartingService.WebApi.Models;

namespace Module_02.Task_01.CartingService.WebApi.Configurations;

public static class FluentValidationExtensions
{
    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(configuration =>
        {
            configuration.DisableDataAnnotationsValidation = false;
        });
        services.AddFluentValidationClientsideAdapters();

        services.AddValidatorsFromAssemblyContaining<ErrorModelResponse>(ServiceLifetime.Scoped);

        return services;
    }

}