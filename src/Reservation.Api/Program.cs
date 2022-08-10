using Kros.KORM.Extensions.Asp;
using Reservation.Api;
using Reservation.Domains;
using Reservation.Domains.Infrastructure;
using Reservation.Domains.ValueObjects;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddKorm(builder.Configuration)
    .UseDatabaseConfiguration(new DatabaseConfiguration())
    .AddKormMigrations(o =>
    {
        o.AddAssemblyScriptsProvider(typeof(DatabaseConfiguration).Assembly, "Reservation.Domains.SqlScripts");
    })
    .MigrateWithRetrying();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/api/companies", async (NewCompany company) =>
{
    var newCompany = Company.CreateNewCompany(company.Name, company.WebName, new ReservationSettings(), new Checker());
    await Task.CompletedTask;
    return Results.Created("/api/companies/" + newCompany.Id, new { id = newCompany.Id });
}).WithTags("Companies");

app.MapGet("/api/companies/{id}", (Guid id) => new CompanyId(id)).WithTags("Companies");

app.MapGet("/api/companies/names/{name}", (string name) => (WebName)name).WithTags("Companies");

app.MapGet("/api/companies/{companyId}/reservations/freedays", (Guid companyId, DateOnly @from, DateOnly to) => new string[] {@from.ToShortDateString(), to.ToShortDateString(), "piatok", "pondelok" })
    .WithTags("Reservations");

app.MapGet("/api/companies/{companyId}/reservations/freeslots",
    (Guid companyId, DateOnly date) => new string[] { date.ToShortDateString(), "8:00", "9:00" })
    .WithTags("Reservations");

app.MapPost("/api/companies/{companyId}/reservations", async (Guid companyId, NewReservation reservation) =>
{
    await Task.CompletedTask;
    var id = ReservationId.New();
    return Results.Created($"/api/companies/{companyId}/reservations/{id}" , new { id = id });
}).WithTags("Reservations");

app.Run();

public record NewCompany(string WebName, string Name);

public record NewReservation(DateTime Date, TimeSpan Start, TimeSpan End);