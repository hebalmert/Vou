using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Vou.Web;
using Vou.Web.Auth;
using Vou.Web.Helpers;
using Vou.Web.Repositories;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//Implementacion del Blazor Bootstrap
builder.Services.AddBlazorBootstrap();

//Para unir Blazor con el API, se agregar la URL de donde estara nuestro API
builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7246") });

builder.Services.AddScoped<IRepository, Repository>();

//Para el manejo del SweetAlert2
builder.Services.AddSweetAlert2(options => {
    options.Theme = SweetAlertTheme.Default;
});

//Para el sistema de autenticacion de usuario y manejo del Provider test authentication
builder.Services.AddAuthorizationCore();
//Era solo para hacer pruebas
//builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderTest>();

builder.Services.AddScoped<AuthenticationProviderJWT>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());

builder.Services.AddScoped<ILoginService, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());


await builder.Build().RunAsync();
