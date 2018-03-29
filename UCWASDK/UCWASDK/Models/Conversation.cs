using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading;
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

        public Task AddParticipant(string to)
        {
            return AddParticipant(to, HttpService.GetNewCancellationToken());
        }
        public async Task AddParticipant(string to, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(to))
                return;
            
            JObject body = new JObject();
            body["to"] = to;
            body["operationid"] = Guid.NewGuid();

            await HttpService.Post(Links.addParticipant, body, cancellationToken);
        }

        public Task<ApplicationSharing> GetApplicationSharing()
        {
            return GetApplicationSharing(HttpService.GetNewCancellationToken());
        }
        public async Task<ApplicationSharing> GetApplicationSharing(CancellationToken cancellationToken)
        {
            return await HttpService.Get<ApplicationSharing>(Links.applicationSharing, cancellationToken);
        }

        public Task<Attendees> GetAttendees()
        {
            return GetAttendees(HttpService.GetNewCancellationToken());
        }
        public async Task<Attendees> GetAttendees(CancellationToken cancellationToken)
        {
            return await HttpService.Get<Attendees>(Links.attendees, cancellationToken);
        }

        public Task<AudioVideo> GetAudioVideo()
        {
            return GetAudioVideo(HttpService.GetNewCancellationToken());
        }
        public async Task<AudioVideo> GetAudioVideo(CancellationToken cancellationToken)
        {
            return await HttpService.Get<AudioVideo>(Links.audioVideo, cancellationToken);
        }

        public Task<DataCollaboration> GetDataCollaboration()
        {
            return GetDataCollaboration(HttpService.GetNewCancellationToken());
        }
        public async Task<DataCollaboration> GetDataCollaboration(CancellationToken cancellationToken)
        {
            return await HttpService.Get<DataCollaboration>(Links.dataCollaboration, cancellationToken);
        }

        public Task DisableAudienceMessaging()
        {
            return DisableAudienceMessaging(HttpService.GetNewCancellationToken());
        }
        public async Task DisableAudienceMessaging(CancellationToken cancellationToken)
        {
            await HttpService.Post(Links.disableAudienceMessaging, "", cancellationToken);
        }

        public Task DisableAudienceMuteLock()
        {
            return DisableAudienceMuteLock(HttpService.GetNewCancellationToken());
        }
        public async Task DisableAudienceMuteLock(CancellationToken cancellationToken)
        {
            await HttpService.Post(Links.disableAudienceMuteLock, "", cancellationToken);
        }

        public Task EnableAudienceMessaging()
        {
            return EnableAudienceMessaging(HttpService.GetNewCancellationToken());
        }
        public async Task EnableAudienceMessaging(CancellationToken cancellationToken)
        {
            await HttpService.Post(Links.enableAudienceMessaging, "", cancellationToken);
        }

        public Task EnableAudienceMuteLock()
        {
            return EnableAudienceMuteLock(HttpService.GetNewCancellationToken());
        }
        public async Task EnableAudienceMuteLock(CancellationToken cancellationToken)
        {
            await HttpService.Post(Links.enableAudienceMuteLock, "", cancellationToken);
        }

        public Task Delete()
        {
            return Delete(HttpService.GetNewCancellationToken());
        }
        public async Task Delete(CancellationToken cancellationToken)
        {
            var uri = Self;
            await HttpService.Delete(uri, cancellationToken);
        }

        public Task<Leaders> GetLeaders()
        {
            return GetLeaders(HttpService.GetNewCancellationToken());
        }
        public async Task<Leaders> GetLeaders(CancellationToken cancellationToken)
        {
            return await HttpService.Get<Leaders>(Links.leaders, cancellationToken);
        }

        public Task<Lobby> GetLobby()
        {
            return GetLobby(HttpService.GetNewCancellationToken());
        }
        public async Task<Lobby> GetLobby(CancellationToken cancellationToken)
        {
            return await HttpService.Get<Lobby>(Links.lobby, cancellationToken);
        }

        public Task<LocalParticipant> GetLocalParticipant()
        {
            return GetLocalParticipant(HttpService.GetNewCancellationToken());
        }
        public async Task<LocalParticipant> GetLocalParticipant(CancellationToken cancellationToken)
        {
            return await HttpService.Get<LocalParticipant>(Links.localParticipant, cancellationToken);
        }

        public Task<Messaging> GetMessaging()
        {
            return GetMessaging(HttpService.GetNewCancellationToken());
        }
        public async Task<Messaging> GetMessaging(CancellationToken cancellationToken)
        {
            return await HttpService.Get<Messaging>(Links.messaging, cancellationToken);
        }

        public Task<OnlineMeeting> GetOnlineMeeting()
        {
            return GetOnlineMeeting(HttpService.GetNewCancellationToken());
        }
        public async Task<OnlineMeeting> GetOnlineMeeting(CancellationToken cancellationToken)
        {
            return await HttpService.Get<OnlineMeeting>(Links.onlineMeeting, cancellationToken);
        }

        public Task<PhoneAudio> GetPhoneAudio()
        {
            return GetPhoneAudio(HttpService.GetNewCancellationToken());
        }
        public async Task<PhoneAudio> GetPhoneAudio(CancellationToken cancellationToken)
        {
            return await HttpService.Get<PhoneAudio>(Links.phoneAudio, cancellationToken);
        }
    }
}
