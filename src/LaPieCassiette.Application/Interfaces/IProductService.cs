using System;
using LaPieCassiette.Domain.Models;
using LaPieCassiette.Application.DTOs;
public interface IProductService
{
    List<Product> GetProducts();
    Product GetById(int id);
    
    void Update(int id, ProductDto dto);
    void Delete(int id);
    void Create(ProductDto dto, Stream? imageStream, string? fileName);
    void TogglePublish(int id);
}
