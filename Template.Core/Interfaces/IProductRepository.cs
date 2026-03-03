using Template.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Template.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<(List<Product> Products, int TotalCount)> GetAllProducts(int pageNumber, int pageSize);

        Task<Product> GetById(int id);
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int id);
    }
}