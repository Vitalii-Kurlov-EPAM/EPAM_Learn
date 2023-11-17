using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authorization;
using Module_02.Task_02.CatalogService.WebApi.Requirements;
using Module_02.Task_02.CatalogService.WebApi.Static;

namespace Module_02.Task_02.CatalogService.WebApi.Configurations;

public static class KeyCloakAuthExtensions
{
    public static IServiceCollection AddKeyCloakAuth(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        services.AddKeycloakAuthentication(configurationManager);
        services.AddKeycloakAuthorization(configurationManager);
        services.AddAuthorization(options => options.AddPolicy(
            PolicyName.PRODUCTS_READ,
            policyBuilder => policyBuilder
                .RequireAuthenticatedUser()
                .AddRequirements(
                    new RequireProtectedResourceWithVariantsRequirement(
                        variants: new (string resource, string scope)[]
                        {
                            ("products", "buyer:read"),
                            ("products", "manager:read")
                        }
                    )
                ))
        );

        services.AddAuthorization(options => options.AddPolicy(
                PolicyName.PRODUCTS_CREATE,
                policyBuilder => policyBuilder
                    .RequireAuthenticatedUser()
                    .RequireProtectedResource("products", "manager:create")
            )
        );

        services.AddAuthorization(options => options.AddPolicy(
                PolicyName.PRODUCTS_UPDATE,
                policyBuilder => policyBuilder
                    .RequireAuthenticatedUser()
                    .RequireProtectedResource("products", "manager:update")
            )
        );

        services.AddAuthorization(options => options.AddPolicy(
                PolicyName.PRODUCTS_DELETE,
                policyBuilder => policyBuilder
                    .RequireAuthenticatedUser()
                    .RequireProtectedResource("products", "manager:delete")
            )
        );

        services.AddSingleton<IAuthorizationHandler, RequireProtectedResourceWithVariantsRequirementHandler>();

        return services;
    }
}