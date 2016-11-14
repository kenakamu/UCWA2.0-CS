namespace Microsoft.Skype.UCWA.Enums
{
    /// <summary>
    /// Represents the link relationship that should be used to identify the default type of online meeting. The link is used when an online meeting is scheduled.
    /// </summary>
    public enum OnlineMeetingRel
    {
        /// <summary>
        /// The link with the "myAssignedOnlineMeeting" relationship should be used to get the predefined, assigned online meeting.
        /// </summary>
        myAssignedOnlineMeeting,
        /// <summary>
        /// The link with the "myOnlineMeetings" relationship should be used to create a new online meeting.
        /// </summary>
        myOnlineMeetings
    }
}
