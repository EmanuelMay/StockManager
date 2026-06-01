using System.ComponentModel.DataAnnotations;

namespace StockManager.Application.DTO;

public class CreateCategoryDTO
{
    [Required]
    [StringLength(150)]
    public string Name { get; set; } = null!;
}
