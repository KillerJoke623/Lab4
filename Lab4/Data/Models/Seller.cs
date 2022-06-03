using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Lab4.Data.Models;

public class Seller
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(Order = 1)]

    public int ID { get; set; }
    public string Fullname { get; set; }
    public string Position { get; set; }
    
    [DefaultValue("Неизвестна")]
    public string Grade { get; set; }
    
    [DefaultValue(0)]
    public int NumOfSales { get; set; }
    
    [MaybeNull]
    public IEnumerable<CarPart> SСParts { get; set; }
    
    

}