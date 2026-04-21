namespace LaPieCassiette.Domain.Models;

public class Product
{
    public int Id { get; private set; }

    public string Name { get; private set; }
    public string Description { get; private set; }

    public string? ImagePath { get; private set; }

    public bool IsPublished { get; private set; }

    public List<ProductTag> ProductTags { get; private set; } = new();

    private Product() { } // EF

    public Product(string name, string description)
    {
        Name = name;
        Description = description;
        IsPublished = false;
    }

    // 🔹 Update clean
    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }

    // 🔹 Image
    public void SetImage(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Image path cannot be empty");

        ImagePath = path;
    }

    // 🔹 Publication
    public void Publish() => IsPublished = true;
    public void Unpublish() => IsPublished = false;

    // 🔹 Tags (clé pour ton concept)
    public void AddTag(Tag tag)
    {
        if (ProductTags.Any(pt => pt.TagId == tag.Id))
            return;

        ProductTags.Add(new ProductTag(this, tag));
    }
    public void RemoveTag(int tagId)
    {
        var tag = ProductTags.FirstOrDefault(t => t.TagId == tagId);
        if (tag != null)
            ProductTags.Remove(tag);
    }
    public void ClearTags()
    {
        ProductTags.Clear();
    }

   
}