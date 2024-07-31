# Polling Platform Microservice
This .NET 8 microservice encapsulates the domain of a fictional referendum app, whose purpose is to allow a country's legislators to launch a referendum to the population, everything done digitally and over the Internet.

This was built as part of the .NET initiative during my time on the bench at Endava.

## Definitions
**Citizen**: a voter that expresses their political option via a yes/no response to the referendum question 

**Legislator**: the state authority that proposes a law through a referendum question.

## Services

There are currently three key services:

1. **Legislation Service** - CRUP API utilizing controllers that encapsulates & handles proposed laws and referendums for authorized legislators.
2. **Voting Service** - Minimal API that allows citizens to vote on proposed laws & referendums.
3. **Results Service** - Minimal API that allows both legislators and citizens to see the voting results after a referendum is finished.

## Docker
For local development, docker compose is utilized to quickly spin key infrastructure & resources. 

To get started, run the following command:

```bash
docker-compose up -d
```

To bring down the entire stack, run the following command:
```bash
docker-compose down -v
```

The only draw back to using docker compose and an ephemeral DB for local development is that you will need to run EF core migrations to reset the database
```bash
cd Legislation.Data

dotnet ef database update -c LegislationContext -s ../Legislation.Api
```

