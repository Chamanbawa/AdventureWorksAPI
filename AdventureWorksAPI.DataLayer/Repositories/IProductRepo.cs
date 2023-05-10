using AdventureWorksAPI.DataLayer.Models;

namespace AdventureWorksAPI.DataLayer.DataContext;
public interface IProductRepo
{
    public ICollection<Product> GetProducts();
    public ICollection<Product> GetProductsById(int? id);
}
