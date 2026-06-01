using System.ComponentModel.DataAnnotations;

namespace StockManager.Application.DTO;

public class UpdateUserDTO
{
    [StringLength(150)]
    public string? Name { get; set; }

    [StringLength(255)]
    public string? Email { get; set; }
}
