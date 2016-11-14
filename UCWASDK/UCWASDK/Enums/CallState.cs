namespace Microsoft.Skype.UCWA.Enums
{
    /// <summary>
    /// Represents the call state.
    /// </summary>
    public enum CallState
    {
        /// <summary>
        /// Connected.
        /// </summary>
        Connected,
        /// <summary>
        /// Connecting.
        /// </summary>
        Connecting,
        /// <summary>
        /// Disconnected.
        /// </summary>
        Disconnected,
        /// <summary>
        /// Call is in the process of being disconnected.
        /// </summary>
        Disconnecting,
        /// <summary>
        /// Transferring a call.
        /// </summary>
        Transferring
    }
}
