using System;

namespace Microsoft.Skype.UCWA.Models
{
    // Errors: https://msdn.microsoft.com/en-us/skype/ucwa/genericsynchronouserrors
    public interface IUCWAException
    {
        /// <summary>
        /// Provides detailed information about the encountered error
        /// </summary>
        Reason Reason { get; set; }
        /// <summary>
        /// Provides information about whether client should retry or not
        /// </summary>
        bool IsTransient { get; set; }
    }
    public class GenericUCWAException : Exception, IUCWAException
    {
        public GenericUCWAException() : base() { }
        public GenericUCWAException(string message) : base(message) { }
        public GenericUCWAException(string message, Exception innerException) : base(message, innerException) { }
        private Reason _reason;
        public Reason Reason { get => _reason; set => _reason = value; }
        private bool _isTransient;
        public bool IsTransient { get => _isTransient; set => _isTransient = value; }
    }
    public class ApplicationNotFoundException : Exception, IUCWAException
    {
        public ApplicationNotFoundException() : base() { }
        public ApplicationNotFoundException(string message) : base(message) { }
        public ApplicationNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        private Reason _reason;
        public Reason Reason { get => _reason; set => _reason = value; }
        private bool _isTransient;
        public bool IsTransient { get => _isTransient; set => _isTransient = value; }
    }
    public class BadRequestException : Exception, IUCWAException
    {
        public BadRequestException() : base() { }
        public BadRequestException(string message) : base(message) { }
        public BadRequestException(string message, Exception innerException) : base(message, innerException) { }
        private Reason _reason;
        public Reason Reason { get => _reason; set => _reason = value; }
        private bool _isTransient;
        public bool IsTransient { get => _isTransient; set => _isTransient = value; }
    }
    public class DeserializationFailureException : Exception, IUCWAException
    {
        public DeserializationFailureException() : base() { }
        public DeserializationFailureException(string message) : base(message) { }
        public DeserializationFailureException(string message, Exception innerException) : base(message, innerException) { }
        private Reason _reason;
        public Reason Reason { get => _reason; set => _reason = value; }
        private bool _isTransient;
        public bool IsTransient { get => _isTransient; set => _isTransient = value; }
    }
    public class EntityTooLargeException : Exception, IUCWAException
    {
        public EntityTooLargeException() : base() { }
        public EntityTooLargeException(string message) : base(message) { }
        public EntityTooLargeException(string message, Exception innerException) : base(message, innerException) { }
        private Reason _reason;
        public Reason Reason { get => _reason; set => _reason = value; }
        private bool _isTransient;
        public bool IsTransient { get => _isTransient; set => _isTransient = value; }
    }
    public class ExchangeServiceFailureException : Exception, IUCWAException
    {
        public ExchangeServiceFailureException() : base() { }
        public ExchangeServiceFailureException(string message) : base(message) { }
        public ExchangeServiceFailureException(string message, Exception innerException) : base(message, innerException) { }
        private Reason _reason;
        public Reason Reason { get => _reason; set => _reason = value; }
        private bool _isTransient;
        public bool IsTransient { get => _isTransient; set => _isTransient = value; }
    }
    public class InactiveApplicationExpiredException : Exception, IUCWAException
    {
        public InactiveApplicationExpiredException() : base() { }
        public InactiveApplicationExpiredException(string message) : base(message) { }
        public InactiveApplicationExpiredException(string message, Exception innerException) : base(message, innerException) { }
        private Reason _reason;
        public Reason Reason { get => _reason; set => _reason = value; }
        private bool _isTransient;
        public bool IsTransient { get => _isTransient; set => _isTransient = value; }
    }
    public class MethodNotAllowedException : Exception, IUCWAException
    {
        public MethodNotAllowedException() : base() { }
        public MethodNotAllowedException(string message) : base(message) { }
        public MethodNotAllowedException(string message, Exception innerException) : base(message, innerException) { }
        private Reason _reason;
        public Reason Reason { get => _reason; set => _reason = value; }
        private bool _isTransient;
        public bool IsTransient { get => _isTransient; set => _isTransient = value; }
    }
    public class MobileApplicationNoLongerAllowedException : Exception, IUCWAException
    {
        public MobileApplicationNoLongerAllowedException() : base() { }
        public MobileApplicationNoLongerAllowedException(string message) : base(message) { }
        public MobileApplicationNoLongerAllowedException(string message, Exception innerException) : base(message, innerException) { }
        private Reason _reason;
        public Reason Reason { get => _reason; set => _reason = value; }
        private bool _isTransient;
        public bool IsTransient { get => _isTransient; set => _isTransient = value; }
    }
    public class ParameterValidationFailureException : Exception, IUCWAException
    {
        public ParameterValidationFailureException() : base() { }
        public ParameterValidationFailureException(string message) : base(message) { }
        public ParameterValidationFailureException(string message, Exception innerException) : base(message, innerException) { }
        private Reason _reason;
        public Reason Reason { get => _reason; set => _reason = value; }
        private bool _isTransient;
        public bool IsTransient { get => _isTransient; set => _isTransient = value; }
    }
    public class PreconditionFailedException : Exception, IUCWAException
    {
        public PreconditionFailedException() : base() { }
        public PreconditionFailedException(string message) : base(message) { }
        public PreconditionFailedException(string message, Exception innerException) : base(message, innerException) { }
        private Reason _reason;
        public Reason Reason { get => _reason; set => _reason = value; }
        private bool _isTransient;
        public bool IsTransient { get => _isTransient; set => _isTransient = value; }
    }
    public class PreconditionRequiredException : Exception, IUCWAException
    {
        public PreconditionRequiredException() : base() { }
        public PreconditionRequiredException(string message) : base(message) { }
        public PreconditionRequiredException(string message, Exception innerException) : base(message, innerException) { }
        private Reason _reason;
        public Reason Reason { get => _reason; set => _reason = value; }
        private bool _isTransient;
        public bool IsTransient { get => _isTransient; set => _isTransient = value; }
    }
    public class ResourceNotFoundException : Exception, IUCWAException
    {
        public ResourceNotFoundException() : base() { }
        public ResourceNotFoundException(string message) : base(message) { }
        public ResourceNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        private Reason _reason;
        public Reason Reason { get => _reason; set => _reason = value; }
        private bool _isTransient;
        public bool IsTransient { get => _isTransient; set => _isTransient = value; }
    }
    public class ServiceFailureException : Exception, IUCWAException
    {
        public ServiceFailureException() : base() { }
        public ServiceFailureException(string message) : base(message) { }
        public ServiceFailureException(string message, Exception innerException) : base(message, innerException) { }
        private Reason _reason;
        public Reason Reason { get => _reason; set => _reason = value; }
        private bool _isTransient;
        public bool IsTransient { get => _isTransient; set => _isTransient = value; }
    }
    public class ServiceTimeoutException : Exception, IUCWAException
    {
        public ServiceTimeoutException() : base() { }
        public ServiceTimeoutException(string message) : base(message) { }
        public ServiceTimeoutException(string message, Exception innerException) : base(message, innerException) { }
        private Reason _reason;
        public Reason Reason { get => _reason; set => _reason = value; }
        private bool _isTransient;
        public bool IsTransient { get => _isTransient; set => _isTransient = value; }
    }
    public class TooManyApplicationsException : Exception, IUCWAException
    {
        public TooManyApplicationsException() : base() { }
        public TooManyApplicationsException(string message) : base(message) { }
        public TooManyApplicationsException(string message, Exception innerException) : base(message, innerException) { }
        private Reason _reason;
        public Reason Reason { get => _reason; set => _reason = value; }
        private bool _isTransient;
        public bool IsTransient { get => _isTransient; set => _isTransient = value; }
    }
    public class UnsupportedMediaTypeException : Exception, IUCWAException
    {
        public UnsupportedMediaTypeException() : base() { }
        public UnsupportedMediaTypeException(string message) : base(message) { }
        public UnsupportedMediaTypeException(string message, Exception innerException) : base(message, innerException) { }
        private Reason _reason;
        public Reason Reason { get => _reason; set => _reason = value; }
        private bool _isTransient;
        public bool IsTransient { get => _isTransient; set => _isTransient = value; }
    }
    public class VersionNotSupportedException : Exception, IUCWAException
    {
        public VersionNotSupportedException() : base() { }
        public VersionNotSupportedException(string message) : base(message) { }
        public VersionNotSupportedException(string message, Exception innerException) : base(message, innerException) { }
        private Reason _reason;
        public Reason Reason { get => _reason; set => _reason = value; }
        private bool _isTransient;
        public bool IsTransient { get => _isTransient; set => _isTransient = value; }
    }
    public class AuthenticationExpiredException : Exception, IUCWAException
    {
        public override string Message { get { return $"Authentication information : {nameof(TrustedIssuers)} {TrustedIssuers}, {nameof(ClientId)} {ClientId}, {nameof(GrantType)} {GrantType}, {nameof(TokenUri)} {TokenUri}, {nameof(AuthorizationUri)} {AuthorizationUri}, {base.Message}"; } }
        public AuthenticationExpiredException() : base() { }
        public AuthenticationExpiredException(string message) : base(message) { }
        public AuthenticationExpiredException(string message, Exception innerException) : base(message, innerException) { }
        private Reason _reason;
        public Reason Reason { get => _reason; set => _reason = value; }
        private bool _isTransient;
        public bool IsTransient { get => _isTransient; set => _isTransient = value; }
        public string TrustedIssuers { get; internal set; }
        public string ClientId { get; internal set; }
        public string TokenUri { get; internal set; }
        public string GrantType { get; internal set; }
        public string AuthorizationUri { get; internal set; }
    }
}
