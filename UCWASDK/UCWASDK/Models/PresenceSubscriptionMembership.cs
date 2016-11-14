using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the presenceSubscription membership of a single contact. 
    /// This resource captures a unique pair that consists of a contact and a presenceSubscription. If a contact appears in multiple presenceSubscriptions, there will be a separate resource for each membership of this contact. An application can use this to remove a contact from a particular presenceSubscription. 
    /// </summary>
    public class PresenceSubscriptionMembership : UCWAModelBase
    {
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
      
            [JsonProperty("presenceSubscription")]
            internal UCWAHref presenceSubscription { get; set; }
        }

        public async Task<Contact> GetContact()
        {
            return await HttpService.Get<Contact>(Links.contact);
        }

        public async Task<PresenceSubscription> GetPresenceSubscription()
        {
            return await HttpService.Get<PresenceSubscription>(Links.presenceSubscription);
        }

        public async Task RemoveContactFromPresenceSubscription()
        {
            await HttpService.Delete(Self);
        }
    }
}
