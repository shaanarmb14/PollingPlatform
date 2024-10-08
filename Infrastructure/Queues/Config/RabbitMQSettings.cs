﻿namespace Infrastructure.Queues.Config;

public record RabbitMQSettings
{
    public required string Host { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
}
