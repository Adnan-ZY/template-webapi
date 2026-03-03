using Template.Core.Entities;        // for Product
using Template.Core.Interfaces;      // for IProductRepository
using Template.Infrastructure.Data;  // for AppDbContext
using Microsoft.EntityFrameworkCore; // for async EF Core methods like ToListAsync, FindAsync
using System.Collections.Generic;    // for List<Product>
using System.Threading.Tasks;        // for Task<T>

namespace Template.Infrastructure.Repositories;
public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(List<Product> Products, int TotalCount)> GetAllProducts(int pageNumber, int pageSize)
    {
        var totalCount = await _context.Products.CountAsync();

        var skipAmount = (pageNumber - 1) * pageSize;

        var products = await _context.Products.Skip(skipAmount).Take(pageSize).ToListAsync();

        return (products, totalCount);
    }

    public async Task<Product> GetById(int id)
    {
        
        var product = await _context.Products.FindAsync(id);
        if(product == null)
        {
            throw new KeyNotFoundException($"Product with ID {id} not found");

        }
        return product;
    }

    public async Task AddProduct(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateProduct(Product product)
    {
         _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if(product == null)
        {
            throw new KeyNotFoundException($"Product with id {id} not found");
        }
        
        _context.Products.Remove(product);

        await _context.SaveChangesAsync();
    }
}