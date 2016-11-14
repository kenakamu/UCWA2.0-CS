using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the local participants perspective on a multi-modal, multi-party communication. 
    /// A dashboard of the current capabilities that are dynamically aggregated based on the corresponding application's permissions, the user's role, and the capabilities of the remote participants and service components that are involved in the communication. While a conversation can be multi-modal and multi-party, it can also represent a basic call with one remote participant. A conversation is created by the server following an invitation. Note that terminating a conversation simply means that the user is leaving the communication; other participants might still be able to communicate. 
    /// </summary>
    public class Conversation : UCWAModelBase
    {
        [JsonProperty("activeModalities")]
        public ConversationModalityType[] ActiveModalities { get; internal set; }

        [JsonProperty("audienceMessaging")]
        public AudienceMessaging AudienceMessaging { get; internal set; }

        [JsonProperty("audienceMute")]
        public AudienceMuteLock AudienceMute { get; internal set; }

        [JsonProperty("created")]
        public DateTime Created { get; internal set; }

        [JsonProperty("expirationTime")]
        public DateTime ExpirationTime { get; internal set; }

        [JsonProperty("importance")]
        public Importance Importance { get; internal set; }

        [JsonProperty("participantCount")]
        public int ParticipantCount { get; internal set; }

        [JsonProperty("readLocally")]
        public bool ReadLocally { get; internal set; }

        [JsonProperty("recording")]
        public bool recording { get; internal set; }

        [JsonProperty("state")]
        public ConversationState State { get; internal set; }

        [JsonProperty("subject")]
        public string Subject { get; internal set; }

        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("addParticipant")]
            internal AddParticipant addParticipant { get; set; }

            [JsonProperty("applicationSharing")]
            internal UCWAHref applicationSharing { get; set; }

            [JsonProperty("attendees")]
            internal UCWAHref attendees { get; set; }

            [JsonProperty("audioVideo")]
            internal UCWAHref audioVideo { get; set; }

            [JsonProperty("dataCollaboration")]
            internal UCWAHref dataCollaboration { get; set; }

            [JsonProperty("disableAudienceMessaging")]
            internal DisableAudienceMessaging disableAudienceMessaging { get; set; }

            [JsonProperty("disableAudienceMuteLock")]
            internal DisableAudienceMuteLock disableAudienceMuteLock { get; set; }

            [JsonProperty("enableAudienceMessaging")]
            internal EnableAudienceMessaging enableAudienceMessaging { get; set; }

            [JsonProperty("enableAudienceMuteLock")]
            internal EnableAudienceMuteLock enableAudienceMuteLock { get; set; }

            [JsonProperty("leaders")]
            internal UCWAHref leaders { get; set; }

            [JsonProperty("lobby")]
            internal UCWAHref lobby { get; set; }

            [JsonProperty("localParticipant")]
            internal UCWAHref localParticipant { get; set; }

            [JsonProperty("messaging")]
            internal UCWAHref messaging { get; set; }

            [JsonProperty("onlineMeeting")]
            internal UCWAHref onlineMeeting { get; set; }

            [JsonProperty("phoneAudio")]
            internal UCWAHref phoneAudio { get; set; }
        }

        public async Task AddParticipant(string to)
        {
            if (string.IsNullOrEmpty(to))
                return;
            
            JObject body = new JObject();
            body["to"] = to;
            body["operationid"] = Guid.NewGuid();

            await HttpService.Post(Links.addParticipant, body);
        }

        public async Task<ApplicationSharing> GetApplicationSharing()
        {
            return await HttpService.Get<ApplicationSharing>(Links.applicationSharing);
        }

        public async Task<Attendees> GetAttendees()
        {
            return await HttpService.Get<Attendees>(Links.attendees);
        }

        public async Task<AudioVideo> GetAudioVideo()
        {
            return await HttpService.Get<AudioVideo>(Links.audioVideo);
        }

        public async Task<DataCollaboration> GetDataCollaboration()
        {
            return await HttpService.Get<DataCollaboration>(Links.dataCollaboration);
        }

        public async Task DisableAudienceMessaging()
        {
            await HttpService.Post(Links.disableAudienceMessaging, "");
        }

        public async Task DisableAudienceMuteLock()
        {
            await HttpService.Post(Links.disableAudienceMuteLock, "");
        }

        public async Task EnableAudienceMessaging()
        {
            await HttpService.Post(Links.enableAudienceMessaging, "");
        }

        public async Task EnableAudienceMuteLock()
        {
            await HttpService.Post(Links.enableAudienceMuteLock, "");
        }

        public async Task Delete()
        {
            var uri = Self;
            await HttpService.Delete(uri);
        }

        public async Task<Leaders> GetLeaders()
        {
            return await HttpService.Get<Leaders>(Links.leaders);
        }

        public async Task<Lobby> GetLobby()
        {
            return await HttpService.Get<Lobby>(Links.lobby);
        }

        public async Task<LocalParticipant> GetLocalParticipant()
        {
            return await HttpService.Get<LocalParticipant>(Links.localParticipant);
        }

        public async Task<Messaging> GetMessaging()
        {
            return await HttpService.Get<Messaging>(Links.messaging);
        }

        public async Task<OnlineMeeting> GetOnlineMeeting()
        {
            return await HttpService.Get<OnlineMeeting>(Links.onlineMeeting);
        }

        public async Task<PhoneAudio> GetPhoneAudio()
        {
            return await HttpService.Get<PhoneAudio>(Links.phoneAudio);
        }
    }
}
