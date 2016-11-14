namespace Microsoft.Skype.UCWA.Enums
{
    /// <summary>
    /// Represents the different states for attendance announcements in an online meeting.
    /// When attendance announcements are enabled, the online meeting will announce the names of the participants who join the meeting through audio. An application should set this property to EntryExitAnnouncement.Enabled only if the online meeting supports modifying the attendance announcements status.
    /// </summary>
    public enum EntryExitAnnouncement
    {
        /// <summary>
        /// Attendance announcements are disabled.
        /// </summary>
        Disabled,
        /// <summary>
        /// Attendance announcements are enabled.
        /// </summary>
        Enabled,
        /// <summary>
        /// The online meeting does not support modifying attendance announcements. Server versions prior to Microsoft Lync Server 2010 do not support the modification of attendance announcements.
        /// </summary>
        Unsupported
    }
}
