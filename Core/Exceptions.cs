namespace Core;

public class AggregateNotFoundException(string? message) : Exception(message);

public class ConcurrencyException : Exception;
