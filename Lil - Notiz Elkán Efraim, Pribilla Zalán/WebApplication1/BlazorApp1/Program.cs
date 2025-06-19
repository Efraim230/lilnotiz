using BlazorApp1.Service;
using CommonLibrary;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Reflection.Emit;

namespace BlazorApp1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            


            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44363/") // Az API URL-je
            });


            
            builder.Services.AddScoped<ItemService>();
            


            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            


            await builder.Build().RunAsync();
        }
    }
}
