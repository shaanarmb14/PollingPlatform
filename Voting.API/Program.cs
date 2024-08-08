using Infrastructure.Queues.Config;
using MassTransit;
using Voting.Api;
using Auth;

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

var rabbitMQConfig = new RabbitMQSettings() { Host = string.Empty, Username = string.Empty, Password = string.Empty};
builder.Configuration.GetSection("RabbitMq").Bind(rabbitMQConfig);
builder.Services.AddMassTransitWithRabbitMq(rabbitMQConfig);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/vote", async (
    IPublishEndpoint publisher, 
    CancellationToken cancellationToken,
    VoteRequest request
) =>
{
    var validRequest = request.Validate();

    if (!validRequest)
    {
        return Results.BadRequest("Invalid request body");
    }

    var message = request.ToMessage();
    await publisher.Publish(message, cancellationToken);

    return Results.Created();
})
    .WithName("CreateVote")
    .WithOpenApi()
    .RequireAuthorization(Policies.CitizenOnlyPolicy);

app.Run();
