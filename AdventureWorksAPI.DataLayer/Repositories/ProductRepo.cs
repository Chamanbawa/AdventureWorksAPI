using AdventureWorksAPI.DataLayer.Models;

namespace AdventureWorksAPI.DataLayer.DataContext;

public class ProductRepo : IProductRepo
{
    private AdventureWorksLt2019Context _context;

    public ProductRepo(AdventureWorksLt2019Context context)
    {
        _context = context;
    }

    public ICollection<Product> GetProducts()
    {
        return _context.Products.ToList();
    }

    public ICollection<Product> GetProductsById(int? id)
    {
        return _context.Products.Where(p => p.ProductId == id).ToList();
    }
}
