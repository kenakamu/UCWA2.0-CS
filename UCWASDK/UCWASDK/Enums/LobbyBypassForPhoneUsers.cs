namespace Microsoft.Skype.UCWA.Enums
{
    /// <summary>
    /// Represents types of users who bypass the lobby.
    /// An online meeting leader can allow specific types of users to bypass the lobby and be admitted directly into the online meeting, even though under normal conditions those users would be placed in the lobby. For example, an online meeting leader might allow participants who join over the phone to bypass the lobby. Note however, if the current access level of the online meeting is AccessLevel.Locked, all new users who join the online meeting -- regardless of the bypass setting -- are placed in the online meeting lobby.
    /// </summary>
    public enum LobbyBypassForPhoneUsers
    {
        /// <summary>
        /// Lobby bypass is disabled for all participants.
        /// </summary>
        Disabled,
        /// <summary>
        /// Lobby bypass is enabled for participants who join from a voice gateway. For example, PSTN users will bypass the lobby.
        /// </summary>
        Enabled
    }
}
