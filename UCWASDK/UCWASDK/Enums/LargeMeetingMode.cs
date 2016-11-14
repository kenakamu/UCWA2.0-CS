namespace Microsoft.Skype.UCWA.Enums
{
    /// <summary>
    /// The large meeting status of an onlineMeeting.
    /// </summary>
    public enum LargeMeetingMode
    {
        /// <summary>
        /// The online meeting is a normal meeting.
        /// </summary>
        Disabled,
        /// <summary>
        /// The online meeting is a large meeting. This type of meeting ordinarily has a large number of participants.
        /// </summary>
        Enabled,
        /// <summary>
        /// The type of the online meeting cannot be determined.
        /// </summary>
        Unknown
    }
}
