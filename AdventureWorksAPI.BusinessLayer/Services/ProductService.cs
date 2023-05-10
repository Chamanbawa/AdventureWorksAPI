using AdventureWorksAPI.DataLayer;
using AdventureWorksAPI.DataLayer.DataContext;
using AdventureWorksAPI.DataLayer.Models;
using Microsoft.AspNetCore.Http;

namespace AdventureWorksAPI.BusinessLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;

        public ProductService(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        public static IResult CreateProduct(AdventureWorksLt2019Context context, Product product)
        {
            try
            {
                product.Rowguid = Guid.NewGuid();
                product.ModifiedDate = DateTime.Now;
                context.Add(product);
                context.SaveChanges();

                return Results.Created($"/product?id={product.ProductId}", product);
            }
            catch (Exception ex)
            {
                return Results.BadRequest();
            }
        }

        // here we get the repository escribed in DataLayer.Repositories
        public IResult GetProducts()
        {
            var products = _productRepo.GetProducts();
            return Results.Ok(products);
        }

        public static IResult RemoveProduct(AdventureWorksLt2019Context context, int id)
        {
            Product product = context.Products.Where(p => p.ProductId == id).FirstOrDefault();

            if (product == null)
            {
                return Results.NotFound();
            }
            else if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
            return Results.Ok($" Product with Id {product.ProductId} is removed successfully.");
        }

        public static IResult UpdateProduct(AdventureWorksLt2019Context context, int Id, Product? product)
        {
            Product? selectedProduct = context.Products.Find(Id);

            try
            {
                if (selectedProduct == null && product != null)
                {

                    CreateProduct(context, product);
                    return Results.Created($"/product?id={product.ProductId}", product);
                }
                else if (selectedProduct != null)
                {
                    selectedProduct.Name = product.Name;
                    selectedProduct.ProductNumber = product.ProductNumber;
                    selectedProduct.Color = product.Color;
                    selectedProduct.StandardCost= product.StandardCost;
                    selectedProduct.ListPrice= product.ListPrice;
                    selectedProduct.SellStartDate= product.SellStartDate;
                    selectedProduct.Rowguid = Guid.NewGuid();
                    selectedProduct.ModifiedDate = DateTime.Now;

                    context.Products.Update(selectedProduct);
                    context.SaveChanges();

                    //Read(context, selectedProduct.ProductId);
                    return Results.Ok(selectedProduct);
                }
                return Results.Ok();

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        public IResult GetProductDetails(int? id)
        {
            HashSet<Product?> product = _productRepo.GetProductsById(id).ToHashSet();

            if (product == null)
            {
                return Results.NotFound();
            }

            // ** NOT YET IMPLEMENTED OTHER TABLES ** 
            //var result = _productRepo.GetProductsById(id).ToHashSet()
            //.Select(p => new
            //{
            //    product = p.ProductModel.Products.FirstOrDefault(),
            //    productModel = p.ProductModel.Name,
            //    productCategory = p.ProductCategory.Name,
            //    productDescription = p.ProductModel.ProductModelProductDescriptions.Select(p => p.ProductDescription.Description)
            //});

            return Results.Ok(product);
        }

    }
}
