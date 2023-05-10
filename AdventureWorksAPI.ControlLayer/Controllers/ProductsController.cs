using AdventureWorksAPI.BusinessLayer.Services;
using AdventureWorksAPI.DataLayer.DataContext;
using AdventureWorksAPI.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorksAPI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(_productService.GetProducts);
        }
    }
}
