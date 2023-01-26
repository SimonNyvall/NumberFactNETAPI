using NumberFactApiNET.Models;
using System.Net;

namespace NumberFactApiNET.Endpoints;

public static class APIEndpoints
{
    public static void MapAPIEndpoints(this WebApplication app)
    {

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
            if (!(api.Add(factdata) == HttpStatusCode.OK)) return Results.BadRequest();

            return Results.Created("/AddFact", factdata);
        });

        app.MapPost("/AddRandomFact", (FactAPI api) =>
        {
            if (!(api.AddRandomFact() == HttpStatusCode.OK)) return Results.BadRequest();

            return Results.Ok();
        });

        app.MapPut("/UpdateFact/", (FactAPI api, FactData factData, int id) =>
        {
            if (!(api.UpdateFact(factData, id) == HttpStatusCode.OK)) return Results.BadRequest();

            return Results.Ok();
        });

        app.MapDelete("/DeleteFactById/", (FactAPI api, int id) =>
        {
            if (!(api.DeleteFact(id) == HttpStatusCode.OK)) return Results.BadRequest();

            return Results.Ok();
        });
    }
}
