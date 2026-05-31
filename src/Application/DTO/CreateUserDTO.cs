using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StockManager.Application.DTO;

public class CreateUserDTO
{
    [StringLength(150)]
    [Required]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    [EmailAddress]
    [Required]
    public string Email { get; set; } = null!;

    [MinLength(8)]
    [Required]
    [PasswordPropertyText]
    public string Password { get; set; } = null!;
}
