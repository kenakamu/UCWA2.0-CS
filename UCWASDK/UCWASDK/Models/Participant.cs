using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.IO;
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

        public async Task Admit()
        {
            await HttpService.Post(Links.admit, "");
        }

        public async Task<Contact> GetContact()
        {
            return await HttpService.Get<Contact>(Links.contact);
        }

        public async Task<byte[]> GetContactPhoto()
        {
            return await HttpService.GetBinary(Links.contactPhoto);
        }

        public async Task<ContactPresence> GetContactPresence()
        {
            return await HttpService.Get<ContactPresence>(Links.contactPresence);
        }

        public async Task<Conversation> GetConversation()
        {
            return await HttpService.Get<Conversation>(Links.conversation);
        }

        public async Task Demote()
        {
            await HttpService.Post(Links.demote, "");

        }
        public async Task Eject()
        {
            await HttpService.Post(Links.eject, "");
        }

        public async Task<Me> GetMe()
        {
            return await HttpService.Get<Me>(Links.me);
        }

        public async Task<ParticipantApplicationSharing> GetParticipantApplicationSharing()
        {
            return await HttpService.Get<ParticipantApplicationSharing>(Links.participantApplicationSharing);
        }

        public async Task<ParticipantAudio> GetParticipantAudio()
        {
            return await HttpService.Get<ParticipantAudio>(Links.participantAudio);
        }

        public async Task<ParticipantDataCollaboration> GetParticipantDataCollaboration()
        {
            return await HttpService.Get<ParticipantDataCollaboration>(Links.participantDataCollaboration);
        }

        public async Task<ParticipantMessaging> GetParticipantMessaging()
        {
            return await HttpService.Get<ParticipantMessaging>(Links.participantMessaging);
        }

        public async Task<ParticipantPanoramicVideo> GetParticipantPanoramicVideo()
        {
            return await HttpService.Get<ParticipantPanoramicVideo>(Links.participantPanoramicVideo);
        }

        public async Task<ParticipantVideo> GetParticipantVideo()
        {
            return await HttpService.Get<ParticipantVideo>(Links.participantVideo);
        }

        public async Task Promote()
        { 
            await HttpService.Post(Links.promote, "");
        }

        public async Task Reject()
        {
            await HttpService.Post(Links.reject, "");
        }
    }
}
