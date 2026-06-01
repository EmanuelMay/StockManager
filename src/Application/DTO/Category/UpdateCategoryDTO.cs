using System.ComponentModel.DataAnnotations;

namespace StockManager.Application.DTO;

public class UpdateCategoryDTO
{
    [Required]
    [StringLength(150)]
    public string Name { get; set; } = null!;
}
