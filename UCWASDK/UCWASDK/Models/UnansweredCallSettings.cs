using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public async Task<Contact> GetContact()
        {
            return await HttpService.Get<Contact>(Links.contact);
        }

        public async Task ResetUnansweredCallSettings()
        {
            await HttpService.Post(Links.resetUnansweredCallSettings, "");
        }

        public async Task UnansweredCallToContact()
        { 
            await HttpService.Post(Links.unansweredCallToContact, this);
        }

        public async Task UnansweredCallToVoicemail()
        {
            await HttpService.Post(Links.unansweredCallToVoicemail, this);
        }
    }
}
