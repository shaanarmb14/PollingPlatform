namespace SharedInfrastructure.Queues.Contracts;

public record PublishResults(
    int ReferendumID,
    string ReferendumName,
    List<LawDTO> Laws
);

public record LawDTO(string Name, int TotalVotes);