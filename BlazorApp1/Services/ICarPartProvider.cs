using BlazorApp1.Data.Models;

namespace BlazorApp1.Services;

public interface ICarPartProvider
{
    Task<List<CarPart>> GetAll();
    Task<CarPart> GetOne(int id);
    Task<bool> Add(CarPart carPart);
    Task<CarPart> Edit(CarPart item);
    Task<bool> Remove(int id);
}