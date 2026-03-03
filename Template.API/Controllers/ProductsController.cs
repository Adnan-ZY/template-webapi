using Microsoft.AspNetCore.Mvc;
using Template.Application.Services;
using Template.Application.Models;

namespace Template.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] PaginationFilter filter)
        {
            var pagedResult = await _productService.GetAllProducts(filter);
            return Ok(pagedResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
                var product = await _productService.GetProductById(id);
                return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var createdProduct = await _productService.AddProduct(request);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")] // HTTP PUT
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductRequest request)
        {
                await _productService.UpdateProduct(id, request);
                return Ok("Updated Success");// 204 No Content
                
        }
        [HttpDelete("{id}")] // HTTP DELETE
        public async Task<IActionResult> DeleteProduct(int id)
        {
                await _productService.DeleteProduct(id);
                return Ok("Deleted Successfully");

        }
    }
}