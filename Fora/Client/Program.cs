using Blazored.LocalStorage;
using Fora.Client;
using Fora.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IInterestManager, InterestManager>();
builder.Services.AddScoped<IThreadManager, ThreadManager>();

builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
