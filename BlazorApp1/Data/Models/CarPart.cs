namespace BlazorApp1.Data.Models;

public class CarPart
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string BrandOfAuto { get; set; }
    public string ModelOfAuto { get; set; }
    
    
    
    public IEnumerable<Seller> Sellers { get; set; }
}