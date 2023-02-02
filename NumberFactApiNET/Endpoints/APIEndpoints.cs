using NumberFactApiNET.Models;
using System.Net;

namespace NumberFactApiNET.Endpoints;

public static class APIEndpoints
{
    public static void MapAPIEndpoints(this WebApplication app)
    {
        app.MapGet("/ShowAllFacts", async (FactAPI api) =>
        {
            FactData[] factsObject = await api.GetAllFacts();

            return Results.Ok(factsObject);
        });

        app.MapGet("/GetFactById/", async (FactAPI api, string id) =>
        {
            FactData factObject = await api.GetFactById(id);

            return Results.Ok(factObject);
        });

        app.MapPost("/AddFact", (FactAPI api, FactData factdata) =>
        {
            if (!(api.AddFact(factdata) == HttpStatusCode.Created)) return Results.BadRequest();

            return Results.Created("/AddFact", factdata);
        });

        app.MapPost("/AddRandomFact", (FactAPI api) =>
        {
            if (!(api.AddRandomFact() == HttpStatusCode.OK)) return Results.BadRequest();

            return Results.Ok();
        });

        app.MapPut("/UpdateFact/", (FactAPI api, FactData factData, string id) =>
        {
            if (!(api.UpdateFact(factData, id) == HttpStatusCode.OK)) return Results.BadRequest();

            return Results.Ok();
        });

        app.MapDelete("/DeleteFactById/", (FactAPI api, string id) =>
        {
            if (!(api.DeleteFact(id) == HttpStatusCode.OK)) return Results.BadRequest();

            return Results.Ok();
        });
    }
}
