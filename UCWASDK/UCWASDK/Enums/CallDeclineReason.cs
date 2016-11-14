namespace Microsoft.Skype.UCWA.Enums
{
    /// <summary>
    /// Reason for call decline.
    /// </summary>
    public enum CallDeclineReason
    {
        /// <summary>
        /// The call is declined on all endpoints.
        /// </summary>
        Global,
        /// <summary>
        /// The call is declined only on the local endpoint.
        /// </summary>
        Local,
        /// <summary>
        /// ResponseCode was not specified.
        /// </summary>
        Unknown
    }
}
