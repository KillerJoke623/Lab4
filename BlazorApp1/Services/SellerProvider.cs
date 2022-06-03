using System.Net.Http.Json;
using BlazorApp1.Data.Models;
using Newtonsoft.Json;

namespace BlazorApp1.Services;

public class SellerProvider:ISellerProvider
{
    private HttpClient _client;
    public SellerProvider(HttpClient client)
    {
        _client = client;
    }
    public async Task<List<Seller>> GetAll()
    {
        return await _client.GetFromJsonAsync<List<Seller>>("/api/carpart");
    }

    public async Task<Seller> GetOne(int id)
    {
        return await _client.GetFromJsonAsync<Seller>($"/api/carpart/{id}");
    }
    
    public async Task<bool> Add(Seller item)
    {
        string data = JsonConvert.SerializeObject(item);
        StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        var responce = await _client.PostAsync($"/api/carpart", httpContent);
        return await Task.FromResult(responce.IsSuccessStatusCode);
    }

    public async Task<Seller> Edit(Seller item)
    {
        string data = JsonConvert.SerializeObject(item);
        StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        var responce = await _client.PutAsync($"/api/carpart", httpContent);
        Seller carPart = JsonConvert.DeserializeObject<Seller>(responce.Content.ReadAsStringAsync().Result);
        return await Task.FromResult(carPart);
    }

    public async Task<bool> Remove(int id)
    {
        var delete = await _client.DeleteAsync($"/api/carpart/${id}");

        return await Task.FromResult(delete.IsSuccessStatusCode);
    }
}