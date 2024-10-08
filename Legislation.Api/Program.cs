using Infrastructure.Queues.Config;
using Legislation.Api.Extensions;
using Legislation.Api.Law;
using Legislation.Api.Referendum;
using Legislation.Data;
using Microsoft.EntityFrameworkCore;
using Auth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddAuthentication()
    .AddBearerToken();
builder.Services
    .AddAuthorizationBuilder()
    .AddPolicy(Policies.LegislatorOnlyPolicy, policy => policy.RequireRole(Roles.LegislatorRole));

builder.Services.AddDbContext<LegislationContext>(o => 
    o.UseNpgsql(builder.Configuration.GetConnectionString("LegislationContext")));

var rabbitMQConfig = new RabbitMQSettings() { Host = string.Empty, Username = string.Empty, Password = string.Empty };
builder.Configuration.GetSection("RabbitMq").Bind(rabbitMQConfig);
builder.Services.AddMassTransitWithRabbitMq(rabbitMQConfig);

builder.Services.AddTransient<ILawRepository, LawRepository>();
builder.Services.AddTransient<IReferendumRepository, ReferendumRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
