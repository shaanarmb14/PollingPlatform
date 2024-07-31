using MassTransit;
using SharedInfrastructure.Queues.Contracts;

namespace Legislation.Api.Consumers;

public class UpdateVotesConsumer(ILogger<UpdateVotesConsumer> logger) : IConsumer<UpdateVotes>
{
    public Task Consume(ConsumeContext<UpdateVotes> context)
    {
        logger.LogInformation("Updating votes for {LawID} to {Votes}", context.Message.LawID, context.Message.Votes);
        return Task.CompletedTask;
    }
}
