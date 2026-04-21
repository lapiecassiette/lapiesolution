using LaPieCassiette.Domain.Models;
using LaPieCassiette.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

using System;
public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products
            .Include(p => p.ProductTags)
            .ThenInclude(pt => pt.Tag)
            .ToListAsync();
    }

    public async Task<List<Product>> GetPublishedAsync()
    {
        return await _context.Products
            .Where(p => p.IsPublished)
            .Include(p => p.ProductTags)
            .ThenInclude(pt => pt.Tag)
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .Include(p => p.ProductTags)
            .ThenInclude(pt => pt.Tag)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Product>> GetAvailableAsync()
    {
        return await _context.Products
            .Where(p => p.IsPublished)
            .Include(p => p.ProductTags)
            .ThenInclude(pt => pt.Tag)
            .ToListAsync();
    }
}
