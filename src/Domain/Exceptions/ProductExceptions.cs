namespace StockManager.Domain.Exceptions;

public class ProductNotFoundException(string message) : Exception(message);

public class ProductAlreadyExistsException(string message) : Exception(message);