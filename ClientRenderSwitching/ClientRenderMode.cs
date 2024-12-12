using ClientRenderSwitching.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ClientRenderSwitching;

public static class ClientRenderMode
{
    public static IComponentRenderMode ComponentRenderMode =>
        Client.ClientRenderMode.ServerRendering switch
        {
            true => new InteractiveServerRenderMode(false),
            false => new InteractiveWebAssemblyRenderMode(false)
        };

    public static void AddComponents(this IServiceCollection services, bool isProduction)
    {
        if(Client.ClientRenderMode.ServerRendering)
        {
            services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddCircuitOptions(options => { options.DetailedErrors = true; });
        }
        else
        {
            if(isProduction)
            {
                services.AddRazorComponents()
                    .AddInteractiveServerComponents()
                    .AddInteractiveWebAssemblyComponents();
            }
            else
            {
                services.AddRazorComponents()
                    .AddInteractiveServerComponents()
                    .AddCircuitOptions(options => { options.DetailedErrors = true; })
                    .AddInteractiveWebAssemblyComponents();
            }
        }
    }

    public static void AddEndPoints(this WebApplication app)
    {
        if(Client.ClientRenderMode.ServerRendering)
        {
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddAdditionalAssemblies(typeof(Client.ClientRenderMode).Assembly);
        }
        else
        {
            app.MapRazorComponents<App>()
               .AddInteractiveServerRenderMode()
               .AddInteractiveWebAssemblyRenderMode()
               .AddAdditionalAssemblies(typeof(Client.ClientRenderMode).Assembly);
        }
    }
}