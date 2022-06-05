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
    //BUG The type of navigation 'CarPart.Sellers' is '<IntersectByIterator>d__118<Seller, int>' which does not implement 'ICollection<Seller>'. Collection navigations must implement 'ICollection<>' of the target type.
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
        var result = _context.CarParts.Include(cp=>cp.Sellers).FirstOrDefault(cp => cp.Id==id);
        return await Task.FromResult(result);
    }

    public async Task<List<CarPart>> GetCarParts()
    {
        var result = await _context.CarParts.Include(p => p.Sellers).ToListAsync();
        return await Task.FromResult(result);
    }
    //BUG Get rid of Id input Rewrite to Lab5
    //System.InvalidOperationException: The expression '(se.Id == __newCarPart_Id_0)' is invalid inside an 'Include' operation, since it does not represent a property access: 't => t.MyProperty'. To target navigations declared on derived types, use casting ('t => ((Derived)t).MyProperty') or the 'as' operator ('t => (t as Derived).MyProperty'). Collection navigation access can be filtered by composing Where, OrderBy(Descending), ThenBy(Descending), Skip or Take operations. For more information on including related data, see http://go.microsoft.com/fwlink/?LinkID=746393.
    //at Microsoft.EntityFrameworkCore.Query.Internal.NavigationExpandingExpressionVisitor.<ProcessInclude>g__ExtractIncludeFilter|32_0(Expression currentExpression, Expression includeExpression)
    public async Task<CarPart?> UpdateCarPart(int id, CarPart newCarPart)
    {
        
        var carPart = await _context.CarParts.Include(se => se.Id == newCarPart.Id).FirstOrDefaultAsync(cp => cp.Id==id);

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
        var carPart = await _context.CarParts.FirstOrDefaultAsync(s => s.Id == id);
        if (carPart != null)
        {
            DataSource.GetInstance()._carParts.Remove(carPart);
            return true;
        }

        return false;
    }
}