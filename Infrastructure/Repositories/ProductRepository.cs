using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

public class ProductRepository(ApplicationDBContext applicationDBContext) : IProductRepository
{
    private readonly ApplicationDBContext _context = applicationDBContext;

    public async Task AddAsync(Product Product)
    {
        _context.Products.Add(Product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int ProductId)
    {
        var delete = await _context.Products.FindAsync(ProductId);
        _context.RemoveRange(delete);
        await _context.SaveChangesAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task UpdateAsync(Product Product)
    {
        _context.Products.Update(Product);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }
}