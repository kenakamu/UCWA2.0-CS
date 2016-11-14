namespace Microsoft.Skype.UCWA.Enums
{
    /// <summary>
    /// Calls are immediately forwarded. The primary number (the number being forwarded from) does not ring.
    /// </summary>
    public enum ImmediateForwardSettingsTarget
    {
        /// <summary>
        /// Forward calls to a contact.
        /// </summary>
        Contact,
        /// <summary>
        /// Forward calls to delegates.
        /// </summary>
        Delegates,
        /// <summary>
        /// Forward calls to voicemail.
        /// </summary>
        Voicemail
    }
}
