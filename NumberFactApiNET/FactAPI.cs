using Newtonsoft.Json;
using NumberFactApiNET.Models;
using System.Net;

namespace NumberFactApiNET;

public class FactAPI
{
    private readonly HttpClient _httpClient = new();
    public FactAPI()
    {
        _httpClient.BaseAddress = new("https://numberfact.azurewebsites.net/");
    }

    public async Task<FactData[]> GetAllFacts()
    {
        Uri uri = new(_httpClient.BaseAddress!, "/ShowAllFacts");

        HttpResponseMessage response = await _httpClient.GetAsync(uri);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.Content.ToString());
        }

        string json = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<FactData[]>(json)!;
    }

    public async Task<FactData> GetFactById(string id)
    {
        Uri uri = new(_httpClient.BaseAddress!, "/GetFactById/" + id);

        HttpResponseMessage response = await _httpClient.GetAsync(uri);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.Content.ToString());
        }

        string json = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<FactData>(json)!;
    }

    public HttpStatusCode AddFact(FactData factData)
    {
        Uri uri = new(_httpClient.BaseAddress!, "/AddFact");

        HttpResponseMessage response = _httpClient.PostAsJsonAsync(uri, factData).Result;

        return response.StatusCode;
    }

    public HttpStatusCode AddRandomFact()
    {
        Uri uri = new(_httpClient.BaseAddress!, "/AddRandomFact");

        HttpResponseMessage response = _httpClient.PostAsync(uri, null).Result;

        return response.StatusCode;
    }

    public HttpStatusCode UpdateFact(FactData factData, string id)
    {
        Uri uri = new(_httpClient.BaseAddress!, "/UpdateFact/" + id);

        HttpResponseMessage response = _httpClient.PutAsJsonAsync(uri, factData).Result;

        return response.StatusCode;
    }

    public HttpStatusCode DeleteFact(string id)
    {
        Uri uri = new(_httpClient.BaseAddress!, "/DeleteFactById/" + id);

        HttpResponseMessage response = _httpClient.DeleteAsync(uri).Result;

        return response.StatusCode;
    }
}