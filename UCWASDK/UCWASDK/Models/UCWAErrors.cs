using System;

namespace Microsoft.Skype.UCWA.Models
{
    // Errors: https://msdn.microsoft.com/en-us/skype/ucwa/genericsynchronouserrors

    public class ApplicationNotFoundException : Exception
    {
        public ApplicationNotFoundException() : base() { }
        public ApplicationNotFoundException(string message) : base(message) { }
        public ApplicationNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class BadRequestException : Exception
    {
        public BadRequestException() : base() { }
        public BadRequestException(string message) : base(message) { }
        public BadRequestException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class DeserializationFailureException : Exception
    {
        public DeserializationFailureException() : base() { }
        public DeserializationFailureException(string message) : base(message) { }
        public DeserializationFailureException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class EntityTooLargeException : Exception
    {
        public EntityTooLargeException() : base() { }
        public EntityTooLargeException(string message) : base(message) { }
        public EntityTooLargeException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class ExchangeServiceFailureException : Exception
    {
        public ExchangeServiceFailureException() : base() { }
        public ExchangeServiceFailureException(string message) : base(message) { }
        public ExchangeServiceFailureException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class InactiveApplicationExpiredException : Exception
    {
        public InactiveApplicationExpiredException() : base() { }
        public InactiveApplicationExpiredException(string message) : base(message) { }
        public InactiveApplicationExpiredException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class MethodNotAllowedException : Exception
    {
        public MethodNotAllowedException() : base() { }
        public MethodNotAllowedException(string message) : base(message) { }
        public MethodNotAllowedException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class MobileApplicationNoLongerAllowedException : Exception
    {
        public MobileApplicationNoLongerAllowedException() : base() { }
        public MobileApplicationNoLongerAllowedException(string message) : base(message) { }
        public MobileApplicationNoLongerAllowedException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class ParameterValidationFailureException : Exception
    {
        public ParameterValidationFailureException() : base() { }
        public ParameterValidationFailureException(string message) : base(message) { }
        public ParameterValidationFailureException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class PreconditionFailedException : Exception
    {
        public PreconditionFailedException() : base() { }
        public PreconditionFailedException(string message) : base(message) { }
        public PreconditionFailedException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class PreconditionRequiredException : Exception
    {
        public PreconditionRequiredException() : base() { }
        public PreconditionRequiredException(string message) : base(message) { }
        public PreconditionRequiredException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException() : base() { }
        public ResourceNotFoundException(string message) : base(message) { }
        public ResourceNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class ServiceFailureException : Exception
    {
        public ServiceFailureException() : base() { }
        public ServiceFailureException(string message) : base(message) { }
        public ServiceFailureException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class ServiceTimeoutException : Exception
    {
        public ServiceTimeoutException() : base() { }
        public ServiceTimeoutException(string message) : base(message) { }
        public ServiceTimeoutException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class TooManyApplicationsException : Exception
    {
        public TooManyApplicationsException() : base() { }
        public TooManyApplicationsException(string message) : base(message) { }
        public TooManyApplicationsException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class UnsupportedMediaTypeException : Exception
    {
        public UnsupportedMediaTypeException() : base() { }
        public UnsupportedMediaTypeException(string message) : base(message) { }
        public UnsupportedMediaTypeException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class VersionNotSupportedException : Exception
    {
        public VersionNotSupportedException() : base() { }
        public VersionNotSupportedException(string message) : base(message) { }
        public VersionNotSupportedException(string message, Exception innerException) : base(message, innerException) { }
    }

}
