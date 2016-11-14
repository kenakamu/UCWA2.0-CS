using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents a scheduled meeting organized by the user. 
    /// These meetings can be created and updated via the API. The resource captures information about the meeting, including the join URL, the attendees list, and the description. 
    /// </summary>
    public class MyOnlineMeeting : UCWAModelBase
    {
        [JsonProperty("accessLevel")]
        public AccessLevel AccessLevel { get; set; }

        [JsonProperty("attendees")]
        public string[] Attendees { get; set; }

        [JsonProperty("automaticLeaderAssignment")]
        public AutomaticLeaderAssignment AutomaticLeaderAssignment { get; set; }

        [JsonProperty("conferenceId")]
        public string ConferenceId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("entryExitAnnouncement")]
        public EntryExitAnnouncement EntryExitAnnouncement { get; set; }

        [JsonProperty("expirationTime")]
        public DateTime ExpirationTime { get; set; }

        [JsonProperty("joinUrl")]
        public string JoinUrl { get; set; }

        [JsonProperty("leaders")]
        public string[] Leaders { get; set; }

        [JsonProperty("lobbyBypassForPhoneUsers")]
        public LobbyBypassForPhoneUsers LobbyBypassForPhoneUsers { get; set; }

        [JsonProperty("onlineMeetingId")]
        public string OnlineMeetingId { get; set; }

        [JsonProperty("onlineMeetingRel")]
        public OnlineMeetingRel OnlineMeetingRel { get; set; }

        [JsonProperty("onlineMeetingUri")]
        public string OnlineMeetingUri { get; set; }
        
        [JsonProperty("organizerUri")]
        public string OrganizerUri { get; set; }

        [JsonProperty("phoneUserAdmission")]
        public PhoneUserAdmission PhoneUserAdmission { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }
        
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

            [JsonProperty("onlineMeetingExtensions")]
            internal UCWAHref onlineMeetingExtensions { get; set; }
        }

        internal class InternalEmbedded
        {
            [JsonProperty("onlineMeetingExtension")]
            internal OnlineMeetingExtension[] onlineMeetingExtensions { get; set; }               
        }

        public async Task<OnlineMeetingExtensions> GetOnlineMeetingExtensions()
        {
            return await HttpService.Get<OnlineMeetingExtensions>(Links.onlineMeetingExtensions);
        }

        public async Task<MyOnlineMeeting> Get()
        {
            return await HttpService.Get<MyOnlineMeeting>(Self);
        }

        public async Task Delete()
        {
            await HttpService.Delete(Self);
        }

        public async Task<MyOnlineMeeting> Update()
        {
            return await HttpService.Put<MyOnlineMeeting>(Self, this);
        }      
    }
}
