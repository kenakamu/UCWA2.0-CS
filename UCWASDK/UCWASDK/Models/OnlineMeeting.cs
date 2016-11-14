using Microsoft.Skype.UCWA.Enums;
using Newtonsoft.Json;
using System;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents a read-only version of the onlineMeeting associated with this conversation. 
    /// The resource captures information about the meeting, including the join URL, the attendees list, and the description. 
    /// </summary>
    public class OnlineMeeting : UCWAModelBase
    {
        [JsonProperty("accessLevel")]
        public AccessLevel AccessLevel { get; internal set; }

        [JsonProperty("attendees")]
        public string[] Attendees { get; internal set; }

        [JsonProperty("automaticLeaderAssignment")]
        public AutomaticLeaderAssignment AutomaticLeaderAssignment { get; internal set; }

        [JsonProperty("conferenceId")]
        public string ConferenceId { get; internal set; }

        [JsonProperty("description")]
        public string Description { get; internal set; }

        [JsonProperty("entryExitAnnouncement")]
        public EntryExitAnnouncement EntryExitAnnouncement { get; internal set; }

        [JsonProperty("expirationTime")]
        public DateTime ExpirationTime { get; internal set; }

        [JsonProperty("hostingNetwork")]
        public string HostingNetwork { get; internal set; }

        [JsonProperty("joinUrl")]
        public string JoinUrl { get; internal set; }

        [JsonProperty("largeMeeting")]
        public LargeMeetingMode LargeMeeting { get; internal set; }

        [JsonProperty("leaders")]
        public string[] Leaders { get; internal set; }

        [JsonProperty("lobbyBypassForPhoneUsers")]
        public LobbyBypassForPhoneUsers LobbyBypassForPhoneUsers { get; internal set; }

        [JsonProperty("onlineMeetingId")]
        public string OnlineMeetingId { get; internal set; }

        [JsonProperty("onlineMeetingRel")]
        public OnlineMeetingRel OnlineMeetingRel { get; internal set; }

        [JsonProperty("onlineMeetingUri")]
        public string OnlineMeetingUri { get; internal set; }

        [JsonProperty("organizerName")]
        public string OrganizerName { get; internal set; }

        [JsonProperty("organizerUri")]
        public string OrganizerUri { get; internal set; }

        [JsonProperty("phoneUserAdmission")]
        public PhoneUserAdmission PhoneUserAdmission { get; internal set; }

        [JsonProperty("subject")]
        public string Subject { get; internal set; }

        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonProperty("_embedded")]
        internal InternalEmbedded Embedded { get; set; }

        [JsonIgnore]
        public OnlineMeetingExtension[] OnlineMeetingExtensions { get { return Embedded.onlineMeetingExtensions; } }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("conversation")]
            internal UCWAHref conversation { get; set; }

            [JsonProperty("organizer")]
            internal UCWAHref organizer { get; set; }

            [JsonProperty("phoneDialInInformation")]
            internal UCWAHref phoneDialInInformation { get; set; }            
        }

        internal class InternalEmbedded
        {
            [JsonProperty("onlineMeetingExtension")]
            internal OnlineMeetingExtension[] onlineMeetingExtensions { get; set; }
        }
    }
}
