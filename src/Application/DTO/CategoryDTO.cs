using System.ComponentModel.DataAnnotations;

namespace StockManager.Application.DTO;

public class CreateCategoryDTO
{
    [Required]
    public string Name { get; set; } = null!;
}

public class UpdateCategoryDTO
{
    public string? Name { get; set; }
}

public class ResponseCategoryDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
