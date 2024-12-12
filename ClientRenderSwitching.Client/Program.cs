using ClientRenderSwitching.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

if(ClientRenderMode.ClientRendering)
    await builder.Build().RunAsync();
