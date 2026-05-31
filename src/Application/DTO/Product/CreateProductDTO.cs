using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace StockManager.Application.DTO;

public class CreateProductDTO
{
    [Required]
    [StringLength(150)]
    public string Name { get; set; } = null!;

    [Required]
    public int Quantity { get; set; }

    [Required]
    [Precision(10,2)]
    public decimal Price { get; set; }

    [Required]
    public int CategoryId { get; set; }
}
