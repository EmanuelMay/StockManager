namespace StockManager.Domain.Exceptions;

public class CategoryAlreadyExistsException(string message) : Exception(message);

public class CategoryNotFoundException(string message) : Exception(message);
