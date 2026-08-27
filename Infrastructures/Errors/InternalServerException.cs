namespace BasicAuthApi.Infrastructures.Errors;

public class InternalServerException(string? message = null, Exception? innerException = null) : Exception(message, innerException) {}
