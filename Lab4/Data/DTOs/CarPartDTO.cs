namespace Lab4.Data.DTO;

public class CarPartDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string BrandOfAuto { get; set; }
    public string ModelOfAuto { get; set; }
    
    public int[] Sellers { get; set; }
}