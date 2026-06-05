namespace StockManager.Domain.Exceptions;

public class EmailNotConfigured(string message) : Exception(message) { }

public class JWTNotConfigured(string message) : Exception(message) { }
