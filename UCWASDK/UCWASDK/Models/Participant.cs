using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents a remote participant in a conversation. 
    /// A participant resource is the transient representation of a contact that captures attributes such as role and capabilities (e.g. promoting to leader or admitting from lobby). A participant's lifetime is controlled by the server and starts when the participant is present upon joining an onlineMeeting or added later on to a conversation. This resource is removed when the participant leaves the conversation. 
    /// </summary>
    public class Participant : UCWAModelBase
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

            [JsonProperty("admit")]
            internal Admit admit { get; set; }
      
            [JsonProperty("contact")]
            internal UCWAHref contact { get; set; }
      
            [JsonProperty("contactPhoto")]
            internal UCWAHref contactPhoto { get; set; }
      
            [JsonProperty("contactPresence")]
            internal UCWAHref contactPresence { get; set; }

            [JsonProperty("conversation")]
            internal UCWAHref conversation { get; set; }

            [JsonProperty("demote")]
            internal Demote demote { get; set; }
      
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
            
            [JsonProperty("promote")]
            internal Promote promote { get; set; }
      
            [JsonProperty("reject")]
            internal Reject reject { get; set; }
        }

        public Task Admit()
        {
            return Admit(HttpService.GetNewCancellationToken());
        }

        public Task Admit(CancellationToken cancellationToken)
        {
            return HttpService.Post(Links.admit, "", cancellationToken);
        }

        public Task<Contact> GetContact()
        {
            return GetContact(HttpService.GetNewCancellationToken());
        }

        public Task<Contact> GetContact(CancellationToken cancellationToken)
        {
            return HttpService.Get<Contact>(Links.contact, cancellationToken);
        }

        public Task<byte[]> GetContactPhoto()
        {
            return GetContactPhoto(HttpService.GetNewCancellationToken());
        }

        public Task<byte[]> GetContactPhoto(CancellationToken cancellationToken)
        {
            return HttpService.GetBinary(Links.contactPhoto, cancellationToken);
        }

        public Task<ContactPresence> GetContactPresence()
        {
            return GetContactPresence(HttpService.GetNewCancellationToken());
        }

        public Task<ContactPresence> GetContactPresence(CancellationToken cancellationToken)
        {
            return HttpService.Get<ContactPresence>(Links.contactPresence, cancellationToken);
        }

        public Task<Conversation> GetConversation()
        {
            return GetConversation(HttpService.GetNewCancellationToken());
        }

        public Task<Conversation> GetConversation(CancellationToken cancellationToken)
        {
            return HttpService.Get<Conversation>(Links.conversation, cancellationToken);
        }

        public Task Demote()
        {
            return Demote(HttpService.GetNewCancellationToken());
        }

        public Task Demote(CancellationToken cancellationToken)
        {
            return  HttpService.Post(Links.demote, "", cancellationToken);
        }

        public Task Eject()
        {
            return Eject(HttpService.GetNewCancellationToken());
        }

        public Task Eject(CancellationToken cancellationToken)
        {
            return HttpService.Post(Links.eject, "", cancellationToken);
        }

        public Task<Me> GetMe()
        {
            return GetMe(HttpService.GetNewCancellationToken());
        }

        public Task<Me> GetMe(CancellationToken cancellationToken)
        {
            return HttpService.Get<Me>(Links.me, cancellationToken);
        }

        public Task<ParticipantApplicationSharing> GetParticipantApplicationSharing()
        {
            return GetParticipantApplicationSharing(HttpService.GetNewCancellationToken());
        }

        public Task<ParticipantApplicationSharing> GetParticipantApplicationSharing(CancellationToken cancellationToken)
        {
            return HttpService.Get<ParticipantApplicationSharing>(Links.participantApplicationSharing, cancellationToken);
        }

        public Task<ParticipantAudio> GetParticipantAudio()
        {
            return GetParticipantAudio(HttpService.GetNewCancellationToken());
        }

        public Task<ParticipantAudio> GetParticipantAudio(CancellationToken cancellationToken)
        {
            return HttpService.Get<ParticipantAudio>(Links.participantAudio, cancellationToken);
        }

        public Task<ParticipantDataCollaboration> GetParticipantDataCollaboration()
        {
            return GetParticipantDataCollaboration(HttpService.GetNewCancellationToken());
        }

        public Task<ParticipantDataCollaboration> GetParticipantDataCollaboration(CancellationToken cancellationToken)
        {
            return HttpService.Get<ParticipantDataCollaboration>(Links.participantDataCollaboration, cancellationToken);
        }

        public Task<ParticipantMessaging> GetParticipantMessaging(){
            return GetParticipantMessaging(HttpService.GetNewCancellationToken());
        }

        public Task<ParticipantMessaging> GetParticipantMessaging(CancellationToken cancellationToken)
        {
            return HttpService.Get<ParticipantMessaging>(Links.participantMessaging, cancellationToken);
        }

        public Task<ParticipantPanoramicVideo> GetParticipantPanoramicVideo()
        {
            return GetParticipantPanoramicVideo(HttpService.GetNewCancellationToken());
        }

        public Task<ParticipantPanoramicVideo> GetParticipantPanoramicVideo(CancellationToken cancellationToken)
        {
            return HttpService.Get<ParticipantPanoramicVideo>(Links.participantPanoramicVideo, cancellationToken);
        }

        public Task<ParticipantVideo> GetParticipantVideo()
        {
            return GetParticipantVideo(HttpService.GetNewCancellationToken());
        }

        public Task<ParticipantVideo> GetParticipantVideo(CancellationToken cancellationToken)
        {
            return HttpService.Get<ParticipantVideo>(Links.participantVideo, cancellationToken);
        }

        public Task Promote()
        {
            return Promote(HttpService.GetNewCancellationToken());
        }

        public Task Promote(CancellationToken cancellationToken)
        { 
            return HttpService.Post(Links.promote, "", cancellationToken);
        }

        public Task Reject()
        {
            return Reject(HttpService.GetNewCancellationToken());
        }

        public Task Reject(CancellationToken cancellationToken)
        {
            return HttpService.Post(Links.reject, "", cancellationToken);
        }
    }
}
