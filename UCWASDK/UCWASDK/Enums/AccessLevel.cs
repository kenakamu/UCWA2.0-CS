namespace Microsoft.Skype.UCWA.Enums
{
    /// <summary>
    /// The access level defines which users are immediately admitted into the online meeting without being placed into the lobby. An online meeting organizer or leader can select specific types of user to bypass the online meeting lobby (see the LobbyBypassForPhoneUsers enumeration). However, users other than the organizer are always placed in the lobby if the online meeting access level was set to AccessLevel.Locked. 
    /// </summary>
    public enum AccessLevel
    {
        /// <summary>
        /// Everyone is admitted into the online meeting.
        /// </summary>
        Everyone,
        /// <summary>
        /// Only invited participants from the same company are admitted into the online meeting. All other participants are placed in the online meeting lobby.
        /// </summary>
        Invited,
        /// <summary>
        /// Only the organizer is admitted into the online meeting. All other participants are placed in the online meeting lobby.
        /// </summary>
        Locked,
        /// <summary>
        /// Not initialized.
        /// </summary>
        None,
        /// <summary>
        /// Only the participants from the same company are admitted into the online meeting. All other participants are placed in the online meeting lobby.
        /// </summary>
        SameEnterprise
    }
}
