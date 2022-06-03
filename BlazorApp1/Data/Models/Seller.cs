namespace BlazorApp1.Data.Models;

public class Seller
{
    public int ID { get; set; }
    public string Fullname { get; set; }
    public string Position { get; set; }
    
    
    public string Grade { get; set; }
    
    
    public int NumOfSales { get; set; }
    
    
    public IEnumerable<CarPart> SСParts { get; set; }
}