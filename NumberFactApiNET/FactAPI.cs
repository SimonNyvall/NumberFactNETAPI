using Newtonsoft.Json;
using NumberFactApiNET.Models;
using System.Net;

namespace NumberFactApiNET;

public class FactAPI
{
    private readonly Dictionary<int, FactData> factDataList = new();

    private readonly HttpClient _httpClient = new();

    public FactAPI() /*http://localhost:3000/*/
    {
    //https://numberfact.azurewebsites.net/
        _httpClient.BaseAddress = new("http://localhost:3000/");
    }

    public async Task<FactData[]> GetAll()
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

    public async Task<FactData> GetById(string id)
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

    public HttpStatusCode Add(FactData factData)
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