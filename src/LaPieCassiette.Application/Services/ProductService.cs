using System;
using LaPieCassiette.Application.DTOs;
using LaPieCassiette.Domain.Models;

using Microsoft.AspNetCore.Http;
public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public List<Product> GetProducts()
    {
        return _repository.GetAll();
    }

    public Product GetById(int id)
    {
        return _repository.GetById(id);
    }

    public void Create(ProductDto dto, Stream? imageStream, string? fileName)
    {
        var product = new Product(dto.Name, dto.Description, dto.Price);

        if (imageStream != null && !string.IsNullOrEmpty(fileName))
        {
            var extension = Path.GetExtension(fileName);
            var newFileName = Guid.NewGuid() + extension;

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fullPath = Path.Combine(folderPath, newFileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                imageStream.CopyTo(stream);
            }

            product.SetImage("/images/" + newFileName);
            product.SetCategory(dto.Category);
            product.Publish();
        }

        _repository.Add(product);
    }

    public void Update(int id, ProductDto dto)
    {
        var product = _repository.GetById(id);

        if (product == null)
            throw new Exception("Product not found");

        // à améliorer plus tard (méthode dans Domain)
        product = new Product(dto.Name, dto.Description, dto.Price);

        _repository.Update(product);
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
    }

    public void TogglePublish(int id)
    {
        var product = _repository.GetById(id);

        if (product.IsPublished)
            product.Unpublish();
        else
            product.Publish();

        _repository.Update(product);
    }

}