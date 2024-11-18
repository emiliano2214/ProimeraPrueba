using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Empresa.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddHttpClient();
            await builder.Build().RunAsync();
        }
    }
}
