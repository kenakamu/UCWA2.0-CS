namespace Microsoft.Skype.UCWA.Enums
{
    /// <summary>
    /// Error Codes
    /// https://msdn.microsoft.com/en-us/skype/ucwa/errorcode_ref
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// Request Error.
        /// </summary>
        BadRequest,
        /// <summary>
        /// Conflict.
        /// </summary>
        Conflict,
        /// <summary>
        /// The request is too large.
        /// </summary>
        EntityTooLarge,
        /// <summary>
        /// Service failures due to Exchange did not return timely or returned malformed response.This is defined by us and not in standard HttpStatusCode.
        /// </summary>
        ExchangeServiceFailure,
        /// <summary>
        /// Forbidden.
        /// </summary>
        Forbidden,
        /// <summary>
        /// Gateway Timeout.
        /// </summary>
        GatewayTimeout,
        /// <summary>
        /// The resource is permanently gone. The client should not retry.
        /// </summary>
        Gone,
        /// <summary>
        /// Informational.
        /// </summary>
        Informational,
        /// <summary>
        /// Local Failure.
        /// </summary>
        LocalFailure,
        /// <summary>
        /// Method Not allowed.
        /// </summary>
        MethodNotAllowed,
        /// <summary>
        /// Not acceptable here.
        /// </summary>
        NotAcceptable,
        /// <summary>
        /// Resource Not Found.
        /// </summary>
        NotFound,
        /// <summary>
        /// Precondition failed (e.g. ETag mismatch).
        /// </summary>
        PreconditionFailed,
        /// <summary>
        /// A precondition is required.
        /// </summary>
        ReconditionRequired,
        /// <summary>
        /// Remote Failure.
        /// </summary>
        RemoteFailure,
        /// <summary>
        /// Service Unavailable.
        /// </summary>
        ServiceFailure,
        /// <summary>
        /// Service Unavailable.
        /// </summary>
        ServiceUnavailable,
        /// <summary>
        /// Timeout
        /// </summary>
        Timeout,
        /// <summary>
        /// Too many requests.
        /// </summary>
        TooManyRecepient,
        /// <summary>
        /// The content-type is not supported.
        /// </summary>
        UnsupportedMediaType
    }
}
