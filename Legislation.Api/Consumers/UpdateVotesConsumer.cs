using MassTransit;
using SharedInfrastructure.Queues.Contracts;

namespace Legislation.Api.Consumers;

public class UpdateVotesConsumer(ILogger<UpdateVotesConsumer> logger) : IConsumer<UpdateVotes>
{
    public Task Consume(ConsumeContext<UpdateVotes> context)
    {
        logger.LogInformation(
            "Updating votes for {LawID} | No. of Yes Votes {YesVotes} | No. of No Votes {NoVotes}",
            context.Message.LawID, 
            context.Message.YesVotes,
            context.Message.NoVotes
        );
        
        return Task.CompletedTask;
    }
}
