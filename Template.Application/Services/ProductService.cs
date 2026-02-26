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
        
        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProducts();
            return products;
        }
        public async Task<Product> GetProductById(int id)
        {
            var product = await _productRepository.GetById(id);
            return product;
        }
        public async Task AddProduct(Product product)
        {
            await _productRepository.AddProduct(product);
        }
        public async Task UpdateProduct(Product product)
        {

            await _productRepository.UpdateProduct(product);
        }
        public async Task DeleteProduct(int id)
        {

            await _productRepository.DeleteProduct(id);
        }
    }
}