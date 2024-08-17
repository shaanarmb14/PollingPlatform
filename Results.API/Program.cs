using Microsoft.EntityFrameworkCore;
using Results.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ResultContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("VoteContext"))); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/results", () =>
{
    Console.WriteLine("Hello from results minimal api");
})
.WithName("GetReferendumResults")
.WithOpenApi();

app.Run();
