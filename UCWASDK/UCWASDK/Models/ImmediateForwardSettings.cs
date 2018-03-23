using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the settings for a user to immediately forward incoming calls to a specified target. 
    /// The user's incoming calls can be forwarded to a contact, number, delegates, or voicemail, without ringing the user's work number. 
    /// </summary>
    public class ImmediateForwardSettings : UCWAModelBase
    {
        [JsonProperty("target")]
        public ImmediateForwardSettingsTarget Target { get; internal set; }

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
      
            [JsonProperty("immediateForwardToContact")]
            internal ImmediateForwardToContact immediateForwardToContact { get; set; }
      
            [JsonProperty("immediateForwardToDelegates")]
            internal ImmediateForwardToDelegates immediateForwardToDelegates { get; set; }
      
            [JsonProperty("immediateForwardToVoicemail")]
            internal ImmediateForwardToVoicemail immediateForwardToVoicemail { get; set; }
        }

        public Task<Contact> GetContact()
        {
            return GetContact(HttpService.GetNewCancellationToken());
        }
        public async Task<Contact> GetContact(CancellationToken cancellationToken)
        {
            return await HttpService.Get<Contact>(Links.contact, cancellationToken);
        }

        public Task ImmediateForwardToContact()
        {
            return ImmediateForwardToContact(HttpService.GetNewCancellationToken());
        }
        public async Task ImmediateForwardToContact(CancellationToken cancellationToken)
        {
            var uri = Links.immediateForwardToContact.Href + "?target=" + Target;
            await HttpService.Post(uri, "", cancellationToken);
        }

        public Task ImmediateForwardToDelegates()
        {
            return ImmediateForwardToDelegates(HttpService.GetNewCancellationToken());
        }
        public async Task ImmediateForwardToDelegates(CancellationToken cancellationToken)
        {
            await HttpService.Post(Links.immediateForwardToDelegates, "", cancellationToken);
        }

        public Task ImmediateForwardToVoicemail()
        {
            return ImmediateForwardToVoicemail(HttpService.GetNewCancellationToken());
        }
        public async Task ImmediateForwardToVoicemail(CancellationToken cancellationToken)
        {
            await HttpService.Post(Links.immediateForwardToVoicemail, "", cancellationToken);
        }
    }
}
