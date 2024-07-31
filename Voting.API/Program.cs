using MassTransit;
using SharedInfrastructure.Queues.Config;
using Voting.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.MapPost("/vote", (IPublishEndpoint publisher, VoteRequest request) =>
{
    var message = request.ToMessage();
    publisher.Publish(message);
})
.WithName("CreateVote")
.WithOpenApi(); ;

app.Run();
