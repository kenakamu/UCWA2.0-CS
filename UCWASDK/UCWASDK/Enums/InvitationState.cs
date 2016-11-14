namespace Microsoft.Skype.UCWA.Enums
{
    /// <summary>
    /// Represents the status of an outgoing call instance.
    /// </summary>
    public enum InvitationState
    {
        /// <summary>
        /// The outgoing call is in alerting state.
        /// </summary>
        Alerting,
        /// <summary>
        /// The outgoing call was cancelled.
        /// </summary>
        Cancelled,
        /// <summary>
        /// The outgoing call is in connected state.
        /// </summary>
        Connected,
        /// <summary>
        /// The outgoing call is in connecting state.
        /// </summary>
        Connecting,
        /// <summary>
        /// The outgoing call was declined.
        /// </summary>
        Declined,
        /// <summary>
        /// The outgoing call establishment failed.
        /// </summary>
        Failed,
        /// <summary>
        /// The outgoing call got forwarded.
        /// </summary>
        Forwarded,
        /// <summary>
        /// The outgoing call is in team-ringing state.
        /// </summary>
        TeamRing
    }
}
