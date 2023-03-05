using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Service.Order.Application.Consumers;
using Service.Order.Infrastructure;
using Shared.Messages;
using Shared.Services;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
builder.Services.AddControllers(opt => opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy)));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.Authority = builder.Configuration["IdentityServerURL"];
    opt.Audience = "resource_order";
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OrderDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),confg=>confg.MigrationsAssembly("Service.Order.Infrastructure")));
builder.Services.AddMediatR(typeof(Service.Order.Application.Handlers.CreateOrderCommandHandler).Assembly);
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddHttpContextAccessor();

//MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CreateOrderMessageCommandConsumer>().Endpoint(X => X.Name = "create-order-service");
    x.AddConsumer<CourseNameChangedEventConsumer>().Endpoint(X => X.Name = "course-nameChangedEvent-OrderService");
    x.SetKebabCaseEndpointNameFormatter();
    x.UsingRabbitMq((contex, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });
        cfg.ConfigureEndpoints(contex);
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
