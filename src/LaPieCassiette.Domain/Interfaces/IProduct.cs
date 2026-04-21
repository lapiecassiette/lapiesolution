using System;
using System.Collections.Generic;
using LaPieCassiette.Domain.Models;
public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<List<Product>> GetPublishedAsync();

    Task<Product?> GetByIdAsync(int id);

    Task AddAsync(Product product);
    Task UpdateAsync(Product product);

    Task DeleteAsync(int id);
    Task<List<Product>> GetAvailableAsync();
}