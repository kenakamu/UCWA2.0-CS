using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.IO;
using System.Threading;
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
    }
}
