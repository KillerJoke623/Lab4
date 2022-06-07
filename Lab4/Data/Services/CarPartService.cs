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
        CarPart nCarPart = new CarPart()
        {
            Name = carPart.Name,
            Price = carPart.Price,
            BrandOfAuto = carPart.BrandOfAuto,
            ModelOfAuto = carPart.ModelOfAuto
        };
        if (carPart.Sellers.Any())
        {
            nCarPart.Sellers  = _context.Sellers.ToList().IntersectBy(carPart.Sellers, sel => sel.ID).ToList();
        }
        
        var result = _context.CarParts.Add(nCarPart);
        await _context.SaveChangesAsync();
        return await Task.FromResult(result.Entity);
    }
    
    public async Task<CarPart> GetCarPart(int id)
    {
        var result = _context.CarParts.Include(cp=>cp.Sellers).FirstOrDefault(cp => cp.Id==id);
        return await Task.FromResult(result);
    }

    public async Task<List<CarPart>> GetCarParts()
    {
        var result = await _context.CarParts.Include(p => p.Sellers).ToListAsync();
        return await Task.FromResult(result);
    }
    //BUG don't delete Sellers of CarPart
    public async Task<CarPart?> UpdateCarPart(int id, CarPart newCarPart)
    {
        
        var carPart = await _context.CarParts.Include(cp=>cp.Sellers).FirstOrDefaultAsync(cp => cp.Id == id); 
        

        if (carPart != null)
        {
            carPart.Name = newCarPart.Name;
            carPart.Price = newCarPart.Price;
            carPart.ModelOfAuto = newCarPart.ModelOfAuto;
            carPart.BrandOfAuto = newCarPart.BrandOfAuto;
            if (newCarPart.Sellers.Any())
            {
                carPart.Sellers = _context.Sellers.ToList().IntersectBy(newCarPart.Sellers, carp => carp).ToList();
            }
            
            _context.CarParts.Update(carPart);
            _context.Entry(carPart).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return carPart;
        }
        return null;
    }
    
    public async Task<bool> DeleteCarPart(int id)
    {
        var carPart = await _context.CarParts.FirstOrDefaultAsync(cp => cp.Id == id);
        if (carPart != null)
        {
            _context.CarParts.Remove(carPart);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}