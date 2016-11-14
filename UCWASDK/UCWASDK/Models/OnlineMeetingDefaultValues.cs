using Microsoft.Skype.UCWA.Enums;
using Newtonsoft.Json;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the values of myOnlineMeeting properties if not specified at scheduling time.
    /// These default values may be configured by Administrator. 
    /// </summary>
    public class OnlineMeetingDefaultValues : UCWAModelBaseLink
    {
        [JsonProperty("accessLevel")]
        public AccessLevel AccessLevel { get; internal set; }

        [JsonProperty("automaticLeaderAssignment")]
        public AutomaticLeaderAssignment AutomaticLeaderAssignment { get; internal set; }

        [JsonProperty("defaultOnlineMeetingRel")]
        public OnlineMeetingRel DefaultOnlineMeetingRel { get; internal set; }

        [JsonProperty("entryExitAnnouncement")]
        public EntryExitAnnouncement EntryExitAnnouncement { get; internal set; }

        [JsonProperty("lobbyBypassForPhoneUsers")]
        public LobbyBypassForPhoneUsers LobbyBypassForPhoneUsers { get; internal set; }

        [JsonProperty("participantsWarningThreshold")]
        public int ParticipantsWarningThreshold { get; internal set; }
    }
}
