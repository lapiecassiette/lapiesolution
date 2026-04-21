using System;
using LaPieCassiette.Domain.Models;
using LaPieCassiette.Application.DTOs;
public interface IProductService
{
    Task<List<Product>> GetProductsAsync(); // FRONT (publiés)
    Task<List<Product>> GetAllAsync();      // ADMIN (tout)

    Task<Product?> GetByIdAsync(int id);

    Task CreateAsync(ProductDto dto, Stream? imageStream, string? fileName);
    Task UpdateAsync(int id, ProductDto dto, Stream? imageStream, string? fileName);
    Task DeleteAsync(int id);
    Task TogglePublishAsync(int id);
}
