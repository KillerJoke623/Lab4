using BlazorApp1.Data.Models;

namespace BlazorApp1.Services;

public interface ISellerProvider
{
    Task<List<Seller>> GetAll();
    Task<Seller> GetOne(int id);
    Task<bool> Add(Seller seller);
    Task<Seller?> Edit(Seller item);
    Task<bool> Remove(int id);
}