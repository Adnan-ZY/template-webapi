using Template.Application.Models;
using Template.Core.Entities;
using Template.Core.Interfaces;
namespace Template.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public async Task<PagedResult<ProductDto>> GetAllProducts(PaginationFilter filter)
        {
            var (products, totalCount) = await _productRepository.GetAllProducts(filter.PageNumber, filter.PageSize);
            var dtos = products.Select(p => MapToDto(p)).ToList();
            return new PagedResult<ProductDto>
            {
                Data = dtos,
                TotalRecords = totalCount,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };
        }
        public async Task<ProductDto> GetProductById(int id)
        {
            var product = await _productRepository.GetById(id);
            return MapToDto(product);
        }
        public async Task<ProductDto> AddProduct(CreateProductRequest request)
        {
            var newProduct = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow,
            };

            await _productRepository.AddProduct(newProduct);

            return MapToDto(newProduct);
        }   
        public async Task UpdateProduct(int id, UpdateProductRequest request)
        {

            var existingProduct = await _productRepository.GetById(id);

            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with id {id} not found.");
            }

            existingProduct.Name = request.Name;
            existingProduct.Price = request.Price;
            existingProduct.Description = request.Description;

            await _productRepository.UpdateProduct(existingProduct);
        }
        public async Task DeleteProduct(int id)
        {

            await _productRepository.DeleteProduct(id);
        }

        private ProductDto MapToDto(Product product)
        {
            return new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CreatedAt = product.CreatedAt
            };
        }
    }
}