using MassTransit;
using SharedInfrastructure.Queues.Config;
using Voting.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMq"));

builder.Services.AddMassTransitWithRabbitMq();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/vote", (IPublishEndpoint publisher, VoteRequest request) =>
{
    var message = request.ToMessage();
    publisher.Publish(message);
})
.WithName("CreateVote")
.WithOpenApi(); ;

app.Run();
