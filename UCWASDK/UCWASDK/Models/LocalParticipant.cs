using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the user as a local participant in a specific conversation. 
    /// participant is the transient representation of the user that captures her attributes such as role or capabilities (such as promoting to leader or admitting from lobby). A localParticipant's lifetime is controlled by the server and starts when the user joins a conversation. It is removed when the participant leaves the conversation. 
    /// </summary>
    public class LocalParticipant : UCWAModelBase
    {
        [JsonProperty("anonymous")]
        public bool Anonymous { get; internal set; }

        [JsonProperty("name")]
        public string Name { get; internal set; }

        [JsonProperty("organizer")]
        public bool Organizer { get; internal set; }

        [JsonProperty("otherPhoneNumber")]
        public string OtherPhoneNumber { get; internal set; }

        [JsonProperty("role")]
        public Role Role { get; internal set; }

        [JsonProperty("sourceNetwork")]
        public string SourceNetwork { get; internal set; }

        [JsonProperty("uri")]
        public string Uri { get; internal set; }

        [JsonProperty("workPhoneNumber")]
        public string WorkPhoneNumber { get; internal set; }

        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("contact")]
            internal UCWAHref contact { get; set; }
    
            [JsonProperty("contactPhoto")]
            internal UCWAHref contactPhoto { get; set; }
    
            [JsonProperty("contactPresence")]
            internal UCWAHref contactPresence { get; set; }
    
            [JsonProperty("conversation")]
            internal UCWAHref conversation { get; set; }
    
            [JsonProperty("eject")]
            internal Eject eject { get; set; }
    
            [JsonProperty("me")]
            internal UCWAHref me { get; set; }
    
            [JsonProperty("participantApplicationSharing")]
            internal UCWAHref participantApplicationSharing { get; set; }
    
            [JsonProperty("participantAudio")]
            internal UCWAHref participantAudio { get; set; }
    
            [JsonProperty("participantDataCollaboration")]
            internal UCWAHref participantDataCollaboration { get; set; }
    
            [JsonProperty("participantMessaging")]
            internal UCWAHref participantMessaging { get; set; }
    
            [JsonProperty("participantPanoramicVideo")]
            internal UCWAHref participantPanoramicVideo { get; set; }
    
            [JsonProperty("participantVideo")]
            internal UCWAHref participantVideo { get; set; }
        }

        public Task<Contact> GetContact()
        {
            return GetContact(HttpService.GetNewCancellationToken());
        }
        public async Task<Contact> GetContact(CancellationToken cancellationToken)
        {
            return await HttpService.Get<Contact>(Links.contact, cancellationToken);
        }

        public Task<byte[]> GetContactPhone()
        {
            return GetContactPhone(HttpService.GetNewCancellationToken());
        }
        public async Task<byte[]> GetContactPhone(CancellationToken cancellationToken)
        {
            return await HttpService.GetBinary(Links.contactPhoto, cancellationToken);
        }

        public Task<ContactPresence> GetContactPresence()
        {
            return GetContactPresence(HttpService.GetNewCancellationToken());
        }
        public async Task<ContactPresence> GetContactPresence(CancellationToken cancellationToken)
        {
            return await HttpService.Get<ContactPresence>(Links.contactPresence, cancellationToken);
        }

        public Task<Conversation> GetConversation()
        {
            return GetConversation(HttpService.GetNewCancellationToken());
        }
        public async Task<Conversation> GetConversation(CancellationToken cancellationToken)
        {
            return await HttpService.Get<Conversation>(Links.conversation, cancellationToken);
        }

        public Task Eject()
        {
            return Eject(HttpService.GetNewCancellationToken());
        }
        public async Task Eject(CancellationToken cancellationToken)
        {
            await HttpService.Post(Links.eject, "", cancellationToken);
        }

        public Task<Me> GetMe()
        {
            return GetMe(HttpService.GetNewCancellationToken());
        }
        public async Task<Me> GetMe(CancellationToken cancellationToken)
        {
            return await HttpService.Get<Me>(Links.me, cancellationToken);
        }

        public Task<ParticipantApplicationSharing> GetParticipantApplicationSharing()
        {
            return GetParticipantApplicationSharing(HttpService.GetNewCancellationToken());
        }
        public async Task<ParticipantApplicationSharing> GetParticipantApplicationSharing(CancellationToken cancellationToken)
        {
            return await HttpService.Get<ParticipantApplicationSharing>(Links.participantApplicationSharing, cancellationToken);
        }

        public Task<ParticipantAudio> GetParticipantAudio()
        {
            return GetParticipantAudio(HttpService.GetNewCancellationToken());
        }
        public async Task<ParticipantAudio> GetParticipantAudio(CancellationToken cancellationToken)
        {
            return await HttpService.Get<ParticipantAudio>(Links.participantAudio, cancellationToken);
        }

        public Task<ParticipantDataCollaboration> GetParticipantDataCollaboration()
        {
            return GetParticipantDataCollaboration(HttpService.GetNewCancellationToken());
        }
        public async Task<ParticipantDataCollaboration> GetParticipantDataCollaboration(CancellationToken cancellationToken)
        {
            return await HttpService.Get<ParticipantDataCollaboration>(Links.participantDataCollaboration, cancellationToken);
        }

        public Task<ParticipantMessaging> GetParticipantMessaging()
        {
            return GetParticipantMessaging(HttpService.GetNewCancellationToken());
        }
        public async Task<ParticipantMessaging> GetParticipantMessaging(CancellationToken cancellationToken)
        {
            return await HttpService.Get<ParticipantMessaging>(Links.participantMessaging, cancellationToken);
        }

        public Task<ParticipantPanoramicVideo> GetParticipantPanoramicVideo()
        {
            return GetParticipantPanoramicVideo(HttpService.GetNewCancellationToken());
        }
        public async Task<ParticipantPanoramicVideo> GetParticipantPanoramicVideo(CancellationToken cancellationToken)
        {
            return await HttpService.Get<ParticipantPanoramicVideo>(Links.participantPanoramicVideo, cancellationToken);
        }

        public Task<ParticipantVideo> GetParticipantVideo()
        {
            return GetParticipantVideo(HttpService.GetNewCancellationToken());
        }
        public async Task<ParticipantVideo> GetParticipantVideo(CancellationToken cancellationToken)
        {
            return await HttpService.Get<ParticipantVideo>(Links.participantVideo, cancellationToken);
        }
    }
}
