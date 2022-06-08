using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Lab4.Data.Models;

public class CarPart
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(Order = 1)]
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string BrandOfAuto { get; set; }
    public string ModelOfAuto { get; set; }
    
    
    
    public IEnumerable<Seller> Sellers { get; set; }
    //[MaybeNull]
    //public IEnumerable<Customer> Customers { get; set; }
}