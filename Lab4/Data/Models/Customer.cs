using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab4.Data.Models;

public class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(Order = 1)]
    public int ID { get; set; }
    public string FullName { get; set; }
    public string Grade { get; set; }
    
    //public IEnumerable<CarPart> CСParts { get; set; }
    
}