namespace Microsoft.Skype.UCWA.Enums
{
    /// <summary>
    /// An enumeration that indicates the purpose of the associated OnlineMeetingExtension.
    /// </summary>
    public enum OnlineMeetingExtensionType
    {
        /// <summary>
        /// The data in the OnlineMeetingExtension is distributed to the meeting organizer.
        /// </summary>
        RoamedOrganizerData,
        /// <summary>
        /// The data in the OnlineMeetingExtension is distributed to all meeting participants.
        /// </summary>
        RoamedParticipantData,
        /// <summary>
        /// Uninitialized value.
        /// </summary>
        Undefined
    }
}
