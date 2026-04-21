

using System.ComponentModel.DataAnnotations;

namespace LaPieCassiette.Application.DTOs;

public class ProductDto
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty; // 🔥 FIX

    public string? Description { get; set; }

    public bool IsPublished { get; set; }

    public List<int> TagIds { get; set; } = new();

    public string? ExistingImagePath { get; set; }
}