using Gateway.DelegateHandlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile($"configuration.{hostingContext.HostingEnvironment.EnvironmentName.ToLower()}.json").AddEnvironmentVariables();
});


//builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
//{
//    config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
//    .AddJsonFile("appsettings.json", true, true)
//    .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
//    .AddJsonFile($"configuration.{hostingContext.HostingEnvironment.EnvironmentName}.json")
//    .AddEnvironmentVariables();
//});
builder.Services.AddHttpClient<TokenExchangeDelgateHandler>();

builder.Services.AddOcelot().AddDelegatingHandler<TokenExchangeDelgateHandler>();
builder.Services.AddAuthentication().AddJwtBearer("GatewayAuthenticationScheme", opt =>
{
    opt.Authority = builder.Configuration["IdentityServerURL"];
    opt.Audience = "resource_gateway";
});
var app = builder.Build();

await app.UseOcelot();

app.Run();
