using Microsoft.Skype.UCWA.Enums;
using Newtonsoft.Json;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the admin policies for the user's online meetings (myOnlineMeetings).
    /// </summary>
    public class OnlineMeetingPolicies : UCWAModelBaseLink
    {
        [JsonProperty("entryExitAnnouncement")]
        public EntryExitAnnouncement EntryExitAnnouncement { get; internal set; }

        [JsonProperty("externalUserMeetingRecording")]
        public string ExternalUserMeetingRecording { get; internal set; }

        [JsonProperty("meetingRecording")]
        public string MeetingRecording { get; internal set; }

        [JsonProperty("meetingSize")]
        public int MeetingSize { get; internal set; }

        [JsonProperty("phoneUserAdmission")]
        public PhoneUserAdmission PhoneUserAdmission { get; internal set; }

        [JsonProperty("voipAudio")]
        public string VoipAudio { get; internal set; }
    }
}
