namespace Microsoft.Skype.UCWA.Enums
{
    /// <summary>
    /// The target of simultaneous ring call forwarding, if enabled.
    /// </summary>
    public enum SimultaneousRingTarget
    {
        /// <summary>
        /// Simultaneous ring calls are forwarded to a contact.
        /// </summary>
        Contact,
        /// <summary>
        /// Simultaneous ring calls are forwarded to delegates.
        /// </summary>
        Delegates,
        /// <summary>
        /// Simultaneous ring calls are forwarded to other members of the logged-on user's team.
        /// </summary>
        Team
    }
}
