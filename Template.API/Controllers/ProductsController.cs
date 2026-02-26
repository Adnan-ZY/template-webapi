using Microsoft.AspNetCore.Mvc;
using Template.Application.Services;
using Template.Core.Entities;

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
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductById(id);

                return Ok(product);
            }
            catch (KeyNotFoundException ex)
            {

                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _productService.AddProduct(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")] // HTTP PUT
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id)
                return BadRequest("ID mismatch");

            try
            {
                await _productService.UpdateProduct(product);
                return NoContent(); // 204 No Content
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // 404 if product not found
            }
        }
        [HttpDelete("{id}")] // HTTP DELETE
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProduct(id);
                return NoContent(); // 204 after successful deletion
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // 404 if not found
            }
        }
    }
}