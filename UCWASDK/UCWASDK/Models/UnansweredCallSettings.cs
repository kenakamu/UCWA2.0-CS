using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents a user's settings to send unanswered calls to a specified target.
    /// The user's incoming calls can be sent to a contact, number, delegates, or team members, if the user does not respond. 
    /// </summary>
    public class UnansweredCallSettings : UCWAModelBase
    {
        [JsonProperty("ringDelay")]
        public int RingDelay { get; set; }

        [JsonProperty("target")]
        public UnansweredCallHandlingTarget Target { get; set; }

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
     
            [JsonProperty("resetUnansweredCallSettings")]
            internal ResetUnansweredCallSettings resetUnansweredCallSettings { get; set; }
     
            [JsonProperty("unansweredCallToContact")]
            internal UnansweredCallToContact unansweredCallToContact { get; set; }
     
            [JsonProperty("unansweredCallToVoicemail")]
            internal UnansweredCallToVoicemail unansweredCallToVoicemail { get; set; }
        }

        public Task<Contact> GetContact()
        {
            return GetContact(HttpService.GetNewCancellationToken());
        }

        public Task<Contact> GetContact(CancellationToken cancellationToken)
        {
            return HttpService.Get<Contact>(Links.contact, cancellationToken);
        }

        public Task ResetUnansweredCallSettings()
        {
            return ResetUnansweredCallSettings(HttpService.GetNewCancellationToken());
        }

        public async Task ResetUnansweredCallSettings(CancellationToken cancellationToken)
        {
            await HttpService.Post(Links.resetUnansweredCallSettings, "", cancellationToken);
        }

        public Task UnansweredCallToContact()
        {
            return UnansweredCallToContact(HttpService.GetNewCancellationToken());
        }

        public Task UnansweredCallToContact(CancellationToken cancellationToken)
        { 
            return HttpService.Post(Links.unansweredCallToContact, this, cancellationToken);
        }

        public Task UnansweredCallToVoicemail()
        {
            return UnansweredCallToVoicemail(HttpService.GetNewCancellationToken());
        }

        public Task UnansweredCallToVoicemail(CancellationToken cancellationToken)
        {
            return HttpService.Post(Links.unansweredCallToVoicemail, this, cancellationToken);
        }
    }
}
