using MassTransit;
using Microsoft.Extensions.Options;
using Voting.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMq"));

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.UsingRabbitMq((ctx, cfg) =>
    {
        var config = ctx.GetRequiredService<IOptions<RabbitMQSettings>>().Value;
        cfg.Host(config.Host, "/", h =>
        {
            h.Username(config.Username);
            h.Password(config.Password);
        });
        cfg.ConfigureEndpoints(ctx);
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

app.MapPost("/vote", (IPublishEndpoint publisher, VoteRequest request) =>
{
    var message = request.ToMessage();
    publisher.Publish(message);
})
.WithName("CreateVote")
.WithOpenApi(); ;

app.Run();
