namespace Microsoft.Skype.UCWA.Enums
{
    /// <summary>
    /// Defines which users are automatically promoted to leaders when they join the meeting.
    /// </summary>
    public enum AutomaticLeaderAssignment
    {
        /// <summary>
        /// No one is automatically promoted to leader. Pre-invited leaders can still join the online meeting as leaders.
        /// </summary>
        Disabled,
        /// <summary>
        /// Everyone is automatically promoted to leader on joining the online meeting.
        /// </summary>
        Everyone,
        /// <summary>
        /// Everyone from the same company is automatically promoted to leader.
        /// </summary>
        SameEnterprise
    }
}
