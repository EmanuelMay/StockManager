using System.ComponentModel.DataAnnotations;

namespace StockManager.Application.DTO;

public class UpdateProductDTO
{
    [StringLength(150)]
    public string? Name { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public int? CategoryId { get; set; }
}
