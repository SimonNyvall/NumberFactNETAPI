using Newtonsoft.Json;
using NumberFactApiNET.Models;
using System.Net;

namespace NumberFactApiNET;

public class FactAPI
{
    private readonly Dictionary<int, FactData> factDataList = new();

    private readonly HttpClient _httpClient = new();

    public FactAPI(HttpClient httpClient)
    {

    }

    public async Task<FactData[]> GetAll()
    {
        var response = await _httpClient.GetAsync("http://localhost:3000/ShowAllFacts");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.Content.ToString());
        }

        string json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<FactData[]>(json);
    }

    public async Task<FactData> GetById(int id)
    {
        var response = await _httpClient.GetAsync("http://localhost:3000/GetFactById/" + id);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.Content.ToString());
        }

        string json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<FactData>(json);
    }

    public HttpStatusCode Add(FactData factData)
    {
        var response = _httpClient.PostAsJsonAsync("http://localhost:3000/AddFact", factData).Result;

        return response.StatusCode;
    }

    public HttpStatusCode AddRandomFact()
    {
        var response = _httpClient.PostAsync("http://localhost:3000/AddRandomFact", null).Result;

        return response.StatusCode;
    }

    public HttpStatusCode UpdateFact(FactData factData, int id)
    {
        var response = _httpClient.PutAsJsonAsync("http://localhost:3000/UpdateFact/" + id, factData).Result;

        return response.StatusCode;
    }

    public HttpStatusCode DeleteFact(int id)
    {
        var response = _httpClient.DeleteAsync("http://localhost:3000/DeleteFactById/" + id).Result;

        return response.StatusCode;
    }
}
