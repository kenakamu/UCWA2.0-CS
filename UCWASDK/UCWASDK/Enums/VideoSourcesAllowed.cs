namespace Microsoft.Skype.UCWA.Enums
{
    /// <summary>
    /// Indicates which participants are allowed to contribute video sources in an online meeting.
    /// </summary>
    public enum VideoSourcesAllowed
    {
        /// <summary>
        /// Everyone can contribute video into a conference.
        /// </summary>
        Everyone,
        /// <summary>
        /// Only one designated participant can contribute video into a conference.
        /// </summary>
        OneParticipant,
        /// <summary>
        /// Only presenters can contribute video into a conference.
        /// </summary>
        PresentersOnly,
        /// <summary>
        /// Allowed video source is unknown.
        /// </summary>
        Unknown
    }
}
