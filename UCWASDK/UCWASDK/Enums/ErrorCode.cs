namespace Microsoft.Skype.UCWA.Enums
{
    /// <summary>
    /// Error Codes
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
