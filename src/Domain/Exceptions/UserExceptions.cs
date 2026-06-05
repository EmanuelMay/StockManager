namespace StockManager.Domain.Exceptions;

public class UserNotFoundException(string message) : Exception(message);

public class EmailAlreadyExistsException(string message) : Exception(message);

public class InvalidCredentialsException(string message) : Exception(message);

public class InvalidCodeException(string message) : Exception(message);
