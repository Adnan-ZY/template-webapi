using Template.Application.Models;
using System.Threading.Tasks;

namespace Template.Application.Services
{
    public interface IProductService
    {
        // Notice how this line now perfectly matches your ProductService class!
        Task<PagedResult<ProductDto>> GetAllProducts(PaginationFilter filter);

        Task<ProductDto> GetProductById(int id);
        Task<ProductDto> AddProduct(CreateProductRequest request);
        Task UpdateProduct(int id, UpdateProductRequest request);
        Task DeleteProduct(int id);
    }
}