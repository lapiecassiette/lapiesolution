

using System.ComponentModel.DataAnnotations;

namespace LaPieCassiette.Application.DTOs;

public class PlateDto
{
    public List<int> SelectedProductIds { get; set; } = new();
}