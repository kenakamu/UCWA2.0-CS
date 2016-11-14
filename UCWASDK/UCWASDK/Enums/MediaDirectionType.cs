namespace Microsoft.Skype.UCWA.Enums
{
    /// <summary>
    /// An enumeration that represents the media channel direction.
    /// </summary>
    public enum MediaDirectionType
    {
        /// <summary>
        /// The media is inactive.
        /// </summary>
        Inactive,
        /// <summary>
        /// The media is being received.
        /// </summary>
        ReceiveOnly,
        /// <summary>
        /// The media is being sent.
        /// </summary>
        SendOnly,
        /// <summary>
        /// The media is being sent and received.
        /// </summary>
        SendReceive,
        /// <summary>
        /// The media direction is unknown.
        /// </summary>
        Unknonw
    }
}
