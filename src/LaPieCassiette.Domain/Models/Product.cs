namespace LaPieCassiette.Domain.Models;

public class Product
{
    public int Id { get; private set; }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }

    public string? ImagePath { get; private set; }
    public string Category { get; private set; }

    public void SetCategory(string category)
    {
        Category = category;
    }
    public bool IsPublished { get; private set; }

    public Product(string name, string description, decimal price, string category)
    {
        Name = name;
        Description = description;
        Price = price;
        Category = category;
        IsPublished = false;
    }

    public void SetImage(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Image path cannot be empty");

        ImagePath = path;
    }

    public void Update(string name, string description, decimal price)
    {
        Name = name;
        Description = description;
        Price = price;
    }
    public void Update(string name, string description, decimal price, string category)
    {
        Name = name;
        Description = description;
        Price = price;
        Category = category;
    }

    public void Publish() => IsPublished = true;
    public void Unpublish() => IsPublished = false;
}