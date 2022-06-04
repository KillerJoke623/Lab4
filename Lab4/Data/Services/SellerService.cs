using System.Collections;
using Lab4.Data.DTO;
using Lab4.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab4.Data.Services;

public class SellerService
{
    private EducationContext _context;
    public SellerService(EducationContext context)
    {
        _context = context;
    }
    
    //POST
    //BUG The type of navigation 'Seller.ScParts' is '<IntersectByIterator>d__118<CarPart, int>' which does not implement 'ICollection<CarPart>'. Collection navigations must implement 'ICollection<>' of the target type.
    public async Task<Seller> AddSeller(SellerDTO seller)
    {
        /* Лаб4
        DataSource.GetInstance()._sellers.Add(seller);
        return await Task.FromResult(seller);*/
        
        Seller nSeller = new Seller()
        {
            Fullname = seller.Fullname,
            Position = seller.Position,
            Grade = seller.Grade,
            NumOfSales = seller.NumOfSales
        };
        if (seller.SCParts.Any())
        {
            nSeller.ScParts  = _context.CarParts.ToList().IntersectBy(seller.SCParts, part => part.Id);
        }
        
        var result = _context.Sellers.Add(nSeller);
        await _context.SaveChangesAsync();
        return await Task.FromResult(result.Entity);

    }
    

    //GETs
    public async Task<Seller> GetSeller(int id)
    {
        /*Lab4
        var result = DataSource.GetInstance()._sellers.Find(s => s.ID == id);
        return await Task.FromResult(result);
        */
        
        var result = _context.Sellers.Include(s=>s.ScParts).FirstOrDefault(sel => sel.ID==id);
    
        return await Task.FromResult(result);

    }

    public async Task<List<Seller>> GetSellers()
    {
        //Lab4
        //return await Task.FromResult(DataSource.GetInstance()._sellers);
        
        var result = await _context.Sellers.Include(a=>a.ScParts).ToListAsync();
        return await Task.FromResult(result);

    }
    
    //incomplete
    public async Task<List<IncompleteSellerDTO>> GetSellersIncomplete()
    {
        var authors = await _context.Sellers.ToListAsync();
        List<IncompleteSellerDTO> result = new List<IncompleteSellerDTO>();
        authors.ForEach(se=> result.Add(new IncompleteSellerDTO{ID = se.ID, Fullname = se.Fullname, Position = se.Position}));
        return await Task.FromResult(result);
    }


    public async Task<Seller?> UpdateSeller(int id, Seller newSeller)
    {
        /* Lab4
        var seller = DataSource.GetInstance()._sellers.FirstOrDefault(se => se.ID == newSeller.ID);

        if (seller != null)
        {
            seller.Fullname = newSeller.Fullname;
            seller.Position = newSeller.Position;
            seller.Grade = newSeller.Grade;
            seller.NumOfSales = newSeller.NumOfSales;
        }
        return seller;
        */
        var seller = await _context.Sellers.Include(sel=>sel.ScParts).FirstOrDefaultAsync(se => se.ID == id);
        if (seller != null)
        {
            seller.Fullname = newSeller.Fullname;
            seller.Position = newSeller.Position;
            seller.Grade = newSeller.Grade;
            if (newSeller.ScParts.Any())
            {
                seller.ScParts = _context.CarParts.ToList().IntersectBy(newSeller.ScParts, carp => carp).ToList();
            }

            _context.Sellers.Update(seller);
            _context.Entry(seller).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return seller;
        }

        return null;

    }

    public async Task<bool> DeleteSeller(int id)
    {
        /*Lab4
        var seller = DataSource.GetInstance()._sellers.FirstOrDefault(s => s.ID == id);
        if (seller != null)
        {
            DataSource.GetInstance()._sellers.Remove(seller);
            return true;
        }

        return false;
        */
        var seller = await _context.Sellers.FirstOrDefaultAsync(se => se.ID == id);
        if (seller != null)
        {
            _context.Sellers.Remove(seller);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;

    }
}