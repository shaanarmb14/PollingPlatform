namespace Results.Data.ResultEntity;

public static class OutcomeExtensions
{
    public static Outcome ParseFrom(string value) => Enum.TryParse<Outcome>(value, out var result) ? result : Outcome.Unknown;
}

