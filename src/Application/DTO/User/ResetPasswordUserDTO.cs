using System.ComponentModel.DataAnnotations;

namespace StockManager.Application.DTO;

public class ResetPasswordUserDTO
{
    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = null!;

    [Required]
    public string Code { get; set; } = null!;

    [Required]
    [MinLength(8)]
    public string Password { get; set; } = null!;
}
