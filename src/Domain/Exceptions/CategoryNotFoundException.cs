namespace StockManager.Domain.Exceptions;

public class CategoryNotFoundException : Exception
{
    public CategoryNotFoundException(string message) : base(message) { }
}
