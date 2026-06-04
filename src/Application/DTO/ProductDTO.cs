using System.ComponentModel.DataAnnotations;

namespace StockManager.Application.DTO;

public class CreateProductDTO
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public int Quantity { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int CategoryId { get; set; }
}

public class UpdateProductDTO
{
    public string? Name { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public int? CategoryId { get; set; }
}

public class ResponseProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}
