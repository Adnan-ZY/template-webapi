using Template.Application.Models;
using System.Threading.Tasks;

namespace Template.Application.Services
{
    public interface IProductService
    {
        Task<PagedResult<ProductDto>> GetAllProducts(PaginationFilter filter);

        Task<ProductDto> GetProductById(int id);
        Task<ProductDto> AddProduct(CreateProductRequest request);
        Task UpdateProduct(int id, UpdateProductRequest request);
        Task DeleteProduct(int id);
    }
}