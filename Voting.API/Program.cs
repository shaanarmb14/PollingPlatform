using Auth;
using Infrastructure.Queues.Config;
using Microsoft.EntityFrameworkCore;
using Voting.Api;
using Voting.Api.Extensions;
using Voting.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddProblemDetails();

builder.Services
    .AddAuthentication()
    .AddBearerToken();
builder.Services
    .AddAuthorizationBuilder()
    .AddPolicy(Policies.CitizenOnlyPolicy, policy => policy.RequireRole(Roles.CitizenRole));

builder.Services.AddDbContext<VoteContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("VoteContext")));

var rabbitMQConfig = new RabbitMQSettings() { Host = string.Empty, Username = string.Empty, Password = string.Empty};
builder.Configuration.GetSection("RabbitMq").Bind(rabbitMQConfig);
builder.Services.AddMassTransitWithRabbitMq(rabbitMQConfig);

builder.Services.AddTransient<IVoteRepository, VoteRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/vote", (VoteRequest request, IVoteRepository repository) =>
{
    var validRequest = request.Validate();

    if (!validRequest)
    {
        return Results.BadRequest("Invalid request body");
    }

    var newVote = repository.Create(request);

    return Results.Created(string.Empty, newVote);
})
    .WithName("CreateVote")
    .WithOpenApi()
    .RequireAuthorization(Policies.CitizenOnlyPolicy);

app.Run();
