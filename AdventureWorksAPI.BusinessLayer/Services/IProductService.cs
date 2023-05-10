using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorksAPI.DataLayer.DataContext;
using AdventureWorksAPI.DataLayer.Models;
using Microsoft.AspNetCore.Http;

namespace AdventureWorksAPI.BusinessLayer.Services
{
    public interface IProductService
    {
        IResult GetProducts();
        IResult GetProductDetails(int? id);
    }
}
