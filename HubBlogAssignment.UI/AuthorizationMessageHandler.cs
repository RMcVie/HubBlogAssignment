using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public CustomAuthorizationMessageHandler(IAccessTokenProvider provider,
        NavigationManager navigationManager)
        : base(provider, navigationManager)
    {
        ConfigureHandler(
            authorizedUrls: new[] { "https://hubblogazurefunction.azurewebsites.net" },
            scopes: new[] { "https://HubBlog.onmicrosoft.com/b1c766ae-b476-4c93-9988-27067c30dba2/API.Access" }
            );
    }
}