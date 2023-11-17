using Microsoft.OpenApi.Models;

namespace Module_02.Task_02.CatalogService.WebApi.Configurations;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.FullName?.Split('.').Last().Replace("+", "."));

            #region KeyCloak Authentication

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.OpenIdConnect,
                Name = "Authorization",
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                OpenIdConnectUrl = new Uri("http://localhost:8080/realms/epam-learn/.well-known/openid-configuration")

            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });

            #endregion

        });

        return services;
    }

    public static WebApplication UseSwaggerService(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            #region KeyCloak settings

            options.ConfigObject.AdditionalItems.Add("persistAuthorization", "true");
            options.OAuthClientId("module-05");
            options.OAuthRealm("epam-learn");
            options.OAuthClientSecret("FPPXruzHDltuxbX3DQ5QcnwZnoKSu4Lm");
            options.OAuthAppName("Swagger");

            #endregion
        });

        return app;
    }

}