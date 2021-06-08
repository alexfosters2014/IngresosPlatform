using Blazored.LocalStorage;
using IngresosPlatform.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tewr.Blazor.FileReader;
using Radzen;
using MudBlazor.Services;

namespace IngresosPlatform.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl")) });
            builder.Services.AddScoped<IServiceProveedor, ServiceProveedor>();
            builder.Services.AddScoped<IServiceUsuario, ServiceUsuario>();
            builder.Services.AddScoped<IServiceIngreso, ServiceIngreso>();
            builder.Services.AddScoped<IServiceFuncionario, ServiceFuncionario>();
            builder.Services.AddScoped<IServiceArchivo, ServiceArchivo>();
            builder.Services.AddScoped<IServiceIngresoDiario, ServiceIngresoDiario>();

            builder.Services.AddMudServices();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddFileReaderService(options => options.UseWasmSharedBuffer = true);

            await builder.Build().RunAsync();
        }
    }
}
