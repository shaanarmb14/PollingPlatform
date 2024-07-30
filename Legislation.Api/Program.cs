using Legislation.Api.Law;
using Legislation.Api.Referendum;
using Legislation.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LegislationContext>(o => 
    o.UseNpgsql(builder.Configuration.GetConnectionString("LegislationContext")));

builder.Services.AddTransient<ILawRepository, LawRepository>();
builder.Services.AddTransient<IReferendumRepository, ReferendumRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
