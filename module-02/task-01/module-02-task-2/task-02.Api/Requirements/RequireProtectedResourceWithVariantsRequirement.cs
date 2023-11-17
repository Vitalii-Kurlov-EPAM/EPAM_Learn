using Keycloak.AuthServices.Sdk.AuthZ;
using Microsoft.AspNetCore.Authorization;

namespace Module_02.Task_02.CatalogService.WebApi.Requirements;

public class RequireProtectedResourceWithVariantsRequirement : IAuthorizationRequirement
{
    public (string Resource, string Scope)[] Variants { get; }

    public RequireProtectedResourceWithVariantsRequirement(params (string Resource, string Scope)[] variants)
    {
        Variants = variants;
    }
}


public class RequireProtectedResourceWithVariantsRequirementHandler : AuthorizationHandler<RequireProtectedResourceWithVariantsRequirement>
{
    private readonly IKeycloakProtectionClient _client;

    public RequireProtectedResourceWithVariantsRequirementHandler(IKeycloakProtectionClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        RequireProtectedResourceWithVariantsRequirement requirement)
    {
        if (!(context.User.Identity?.IsAuthenticated ?? false))
        {
            return;
        }

        foreach (var (resource, scope) in requirement.Variants)
        {
            var success = await _client.VerifyAccessToResource(resource, scope, CancellationToken.None);

            if (!success)
            {
                continue;
            }

            context.Succeed(requirement);
            return;
        }

        context.Fail();
    }
}