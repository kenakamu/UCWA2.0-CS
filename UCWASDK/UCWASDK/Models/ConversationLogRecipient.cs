using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents a recipient within a conversationLog. 
    /// </summary>
    public class ConversationLogRecipient : UCWAModelBase
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; internal set; }

        [JsonProperty("sipUri")]
        public string SipUri { get; internal set; }

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
        }

        public async Task<Contact> GetContact()
        {
            return await HttpService.Get<Contact>(Links.contact);
        }

        public async Task<byte[]> GetContactPhone()
        {
            return await HttpService.GetBinary(Links.contactPhoto);
        }

        public async Task<ContactPresence> GetContactPresence()
        {
            return await HttpService.Get<ContactPresence>(Links.contactPresence);
        }
    }
}
