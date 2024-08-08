using Legislation.Api.Law;
using MassTransit;
using Infrastructure.Queues.Contracts;

namespace Legislation.Api.Consumers;

public class UpdateVotesConsumer(ILogger<UpdateVotesConsumer> logger, ILawRepository lawRepository) : IConsumer<UpdateVotes>
{
    public Task Consume(ConsumeContext<UpdateVotes> context)
    {
        var (lawID, yesVotes, noVotes) = context.Message;
        logger.LogInformation(
            "Updating votes for {LawID} | No. of Yes Votes {YesVotes} | No. of No Votes {NoVotes}",
            lawID,
            yesVotes,
            noVotes
        );

        return Task.CompletedTask;
    }
}
