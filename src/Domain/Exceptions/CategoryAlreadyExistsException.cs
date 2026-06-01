namespace StockManager.Domain.Exceptions;

public class CategoryAlreadyExistsException : Exception
{
    public CategoryAlreadyExistsException(string message) : base(message) { }
}
