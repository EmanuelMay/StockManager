using System.ComponentModel.DataAnnotations;

namespace StockManager.Application.DTO;

public class UpdateCategoryDTO
{
    [StringLength(150)]
    public string? Name { get; set; }
}
