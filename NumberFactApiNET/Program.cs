using System.Net;
using NumberFactApiNET.Models;
using NumberFactApiNET;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FactAPI>();
builder.Services.AddHttpClient();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/ShowAllFacts", async (FactAPI api) =>
{
    FactData[] factsObject = await api.GetAll();

    return Results.Ok(factsObject);
});

app.MapGet("/GetFactById/", async (FactAPI api, int id) =>
{
    FactData factObject = await api.GetById(id);

    return Results.Ok(factObject);
});

app.MapPost("/AddFact", (FactAPI api, FactData factdata) =>
{
    if (api.Add(factdata) == HttpStatusCode.OK)
    {
        return Results.Created("/AddFact", factdata);
    }
    // New Potato
    return Results.BadRequest();
});

app.MapPost("/AddRandomFact", (FactAPI api) =>
{
    if (api.AddRandomFact() == HttpStatusCode.OK)
    {
        return Results.Ok();
    }

    return Results.BadRequest();
});

app.MapPut("/UpdateFact/", (FactAPI api, FactData factData, int id) =>
{
    if (api.UpdateFact(factData, id) == HttpStatusCode.OK)
    {
        return Results.Ok();
    }

    return Results.BadRequest();
});

app.MapDelete("/DeleteFactById/", (FactAPI api, int id) =>
{
    if (api.DeleteFact(id) == HttpStatusCode.OK)
    {
        return Results.Ok();
    }

    return Results.BadRequest();
});

app.Run();





