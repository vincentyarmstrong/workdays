using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Workdays.Service;
using Blazored.LocalStorage;

namespace Workdays;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        builder.Services.AddTransient<IWorkdayService, WorkdayService>();
        builder.Services.AddTransient<ILocalDataPersistenceService, LocalDataPersistenceService>();
        builder.Services.AddBlazoredLocalStorage();

        await builder.Build().RunAsync();
    }
}