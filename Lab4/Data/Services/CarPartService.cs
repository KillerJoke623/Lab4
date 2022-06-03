using Lab4.Data.DTO;
using Lab4.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab4.Data.Services;

public class CarPartService
{
    private EducationContext _context;
    public CarPartService(EducationContext context)
    {
        _context = context;
    }
    public async Task<CarPart> AddCarPart(CarPartDTO carPart)
    {
        //Lab4
        /*
        DataSource.GetInstance()._carParts.Add(carPart);
        return await Task.FromResult(carPart);
        */
        CarPart nCarPart = new CarPart()
        {
            Name = carPart.Name,
            Price = carPart.Price,
            BrandOfAuto = carPart.BrandOfAuto,
            ModelOfAuto = carPart.ModelOfAuto
        };
        if (carPart.Sellers.Any())
        {
            nCarPart.Sellers  = _context.Sellers.ToList().IntersectBy(carPart.Sellers, sel => sel.ID);
        }
        
        var result = _context.CarParts.Add(nCarPart);
        await _context.SaveChangesAsync();
        return await Task.FromResult(result.Entity);
    }

    public async Task<CarPart> GetCarPart(int id)
    {
        var result = DataSource.GetInstance()._carParts.Find(s => s.Id == id);
        return await Task.FromResult(result);
    }

    public async Task<List<CarPart>> GetCarParts()
    {
        var result = await _context.CarParts.Include(p => p.Sellers).ToListAsync();
        return await Task.FromResult(result);
    }

    public async Task<CarPart?> UpdateCarPart(int id, CarPart newCarPart)
    {
        var carPart = await _context.CarParts.Include(se => se.Id == newCarPart.Id).FirstOrDefaultAsync(cp => cp.Id==id);

        if (carPart != null)
        {
            carPart.Name = newCarPart.Name;
            carPart.Price = newCarPart.Price;
            carPart.ModelOfAuto = newCarPart.ModelOfAuto;
            carPart.BrandOfAuto = newCarPart.BrandOfAuto;
            _context.CarParts.Update(carPart);
            _context.Entry(carPart).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return carPart;
        }
        return carPart;
    }

    public async Task<bool> DeleteCarPart(int id)
    {
        var carPart = await _context.CarParts.FirstOrDefaultAsync(s => s.Id == id);
        if (carPart != null)
        {
            DataSource.GetInstance()._carParts.Remove(carPart);
            return true;
        }

        return false;
    }
}