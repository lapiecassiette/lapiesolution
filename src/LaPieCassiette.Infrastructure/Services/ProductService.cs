using LaPieCassiette.Application.DTOs;
using LaPieCassiette.Domain.Models;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly ITagRepository _tagRepository;

    public ProductService(IProductRepository repository, ITagRepository tagRepository)
    {
        _repository = repository;
        _tagRepository = tagRepository;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await _repository.GetAvailableAsync();
    }
    public async Task<List<Product>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task CreateAsync(ProductDto dto, Stream? imageStream, string? fileName)
    {
        var product = new Product(dto.Name, dto.Description ?? "");

        // IMAGE
        if (imageStream != null && !string.IsNullOrEmpty(fileName))
        {
            var extension = Path.GetExtension(fileName);
            var newFileName = Guid.NewGuid() + extension;

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fullPath = Path.Combine(folderPath, newFileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await imageStream.CopyToAsync(stream);
            }

            product.SetImage("/images/" + newFileName);
        }

        var tags = await _tagRepository.GetAllAsync();

        foreach (var tagId in dto.TagIds)
        {
            var tag = tags.FirstOrDefault(t => t.Id == tagId);
            if (tag != null)
                product.AddTag(tag);
        }

        await _repository.AddAsync(product);
    }
    public async Task UpdateAsync(int id, ProductDto dto, Stream? imageStream, string? fileName)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product == null)
            throw new Exception("Product not found");

        // base
        product.Update(dto.Name, dto.Description ?? "");

        // publish
        if (dto.IsPublished)
            product.Publish();
        else
            product.Unpublish();

        // IMAGE
        if (imageStream != null && !string.IsNullOrEmpty(fileName))
        {
            var extension = Path.GetExtension(fileName);
            var newFileName = Guid.NewGuid() + extension;

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fullPath = Path.Combine(folderPath, newFileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await imageStream.CopyToAsync(stream);
            }

            product.SetImage("/images/" + newFileName);
        }

        // TAGS

        product.ClearTags();

        var tags = await _tagRepository.GetAllAsync();

        foreach (var tagId in dto.TagIds)
        {
            var tag = tags.FirstOrDefault(t => t.Id == tagId);

            if (tag != null)
            {
                product.AddTag(tag);
            }
        }

        await _repository.UpdateAsync(product);
    }
    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task TogglePublishAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product == null)
            throw new Exception("Product not found");

        if (product.IsPublished)
            product.Unpublish();
        else
            product.Publish();

        await _repository.UpdateAsync(product);
    }
}