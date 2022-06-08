using Lab4.Data.Models;

namespace Lab4.Data;

public class DataSource
{
    private static DataSource instance;

    private DataSource()
    {
        
    }

    public static DataSource GetInstance()
    {
        if (instance == null)
        {
            instance = new DataSource();
        }

        return instance;
    }

    public List<Seller> _sellers = new List<Seller>();
    public List<CarPart> _carParts = new List<CarPart>();
    public List<Customer> _customers = new List<Customer>();
}