using System.ComponentModel.DataAnnotations;

namespace StockManager.Application.DTO;

public class UpdateProductDTO
{
    [StringLength(150)]
    public string? Name { get; set; }

    [Range(0, int.MaxValue)]
    public int? Quantity { get; set; }

    [Range(0, int.MaxValue)]
    public decimal? Price { get; set; }

    [Range(0, int.MaxValue)]
    public int? CategoryId { get; set; }
}
