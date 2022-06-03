using System.Net.Http.Json;
using BlazorApp1.Data.Models;
using Newtonsoft.Json;

namespace BlazorApp1.Services;

public class CarPartsProvider:ICarPartProvider
{
    private HttpClient _client;
    public CarPartsProvider(HttpClient client)
    {
        _client = client;
    }
    public async Task<List<CarPart>> GetAll()
    {
        return await _client.GetFromJsonAsync<List<CarPart>>("/api/carpart");
    }

    public async Task<CarPart> GetOne(int id)
    {
        return await _client.GetFromJsonAsync<CarPart>($"/api/carpart/{id}");
    }
    
    public async Task<bool> Add(CarPart item)
    {
        string data = JsonConvert.SerializeObject(item);
        StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        var responce = await _client.PostAsync($"/api/carpart", httpContent);
        return await Task.FromResult(responce.IsSuccessStatusCode);
    }

    public async Task<CarPart> Edit(CarPart item)
    {
        string data = JsonConvert.SerializeObject(item);
        StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        var responce = await _client.PutAsync($"/api/carpart", httpContent);
        CarPart carPart = JsonConvert.DeserializeObject<CarPart>(responce.Content.ReadAsStringAsync().Result);
        return await Task.FromResult(carPart);
    }

    public async Task<bool> Remove(int id)
    {
        var delete = await _client.DeleteAsync($"/api/carpart/${id}");

        return await Task.FromResult(delete.IsSuccessStatusCode);
    }

}