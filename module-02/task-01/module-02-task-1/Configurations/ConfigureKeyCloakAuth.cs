using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Module_02.Task_01.CartingService.WebApi.Static;

namespace Module_02.Task_01.CartingService.WebApi.Configurations;

public static class KeyCloakAuthExtensions
{
    public static IServiceCollection AddKeyCloakAuth(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        services.AddKeycloakAuthentication(configurationManager);
        services.AddKeycloakAuthorization(configurationManager);
        services.AddAuthorization(options => options.AddPolicy(
            PolicyName.CARTS_READ,
            policyBuilder => policyBuilder
                .RequireAuthenticatedUser()
                .RequireResourceRoles("Manager", "Buyer")
                )
        );

        services.AddAuthorization(options => options.AddPolicy(
                PolicyName.CARTS_CREATE,
                policyBuilder => policyBuilder
                    .RequireAuthenticatedUser()
                    .RequireResourceRoles("Manager", "Buyer")
            )
        );

        services.AddAuthorization(options => options.AddPolicy(
                PolicyName.CARTS_UPDATE,
                policyBuilder => policyBuilder
                    .RequireAuthenticatedUser()
                    .RequireResourceRoles("Manager", "Buyer")
            )
        );

        services.AddAuthorization(options => options.AddPolicy(
                PolicyName.CARTS_DELETE,
                policyBuilder => policyBuilder
                    .RequireAuthenticatedUser()
                    .RequireResourceRoles("Manager", "Buyer")
            )
        );

        return services;
    }
}