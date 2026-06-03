using System.ComponentModel.DataAnnotations;

namespace StockManager.Application.DTO;

public class ForgotPasswordUserDTO
{
    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = null!;
}
