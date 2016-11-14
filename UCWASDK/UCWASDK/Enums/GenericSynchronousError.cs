namespace Microsoft.Skype.UCWA.Enums
{
    /// <summary>
    /// Some HTTP requests on UCWA resources can produce a response that indicates an error. This topic lists the common errors that can appear synchronously (in the HTTP response).
    /// The errors listed in the following table apply to most resources.
    /// </summary>
    public enum GenericSynchronousError
    {
        /// <summary>
        /// The application resource does not exist.
        /// </summary>
        ApplicationNotFound,
        /// <summary>
        /// Something is wrong with the entire request (malformed XML/JSON, for example).
        /// </summary>
        BadRequest,
        /// <summary>
        /// The request body could not be deserialized. Please check if the body confirms to allowed formats and does not have any invalid characters.
        /// </summary>
        DeserializationFailure,
        /// <summary>
        /// The request is too large.
        /// </summary>
        EntityTooLarge,
        /// <summary>
        /// Exchange connectivity failure.
        /// </summary>
        ExchangeServiceFailure,
        /// <summary>
        /// An inactive application expired.
        /// </summary>
        InactiveApplicationExpired,
        /// <summary>
        /// The requested HTTP method is not supported for this resource.
        /// </summary>
        MethodNotAllowed,
        /// <summary>
        /// User is no longer authorized for mobile applications.
        /// </summary>
        MobileApplicationNoLongerAllowed,
        /// <summary>
        /// A parameter value is not valid.
        /// </summary>
        ParameterValidationFailure,
        /// <summary>
        /// An If-Match precondition was not met.
        /// </summary>
        PreconditionFailed,
        /// <summary>
        /// The operation requires an If-Match precondition.
        /// </summary>
        PreconditionRequired,
        /// <summary>
        /// The resource does not exist.
        /// </summary>
        ResourceNotFound,
        /// <summary>
        /// Internal Server Error.
        /// </summary>
        ServiceFailure,
        /// <summary>
        /// Internal Server Error, remote timeout.
        /// </summary>
        ServiceTimeout,
        /// <summary>
        /// There are too many applications for this user.
        /// </summary>
        TooManyApplications,
        /// <summary>
        /// The content-type is not supported.
        /// </summary>
        UnsupportedMediaType,
        /// <summary>
        /// Requested Version is not supported.
        /// </summary>
        VersionNotSupported
    }    
}
