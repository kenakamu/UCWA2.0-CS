using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading;
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

        public Task<Contact> GetContact()
        {
            return GetContact(HttpService.GetNewCancellationToken());
        }

        public Task<Contact> GetContact(CancellationToken cancellationToken)
        {
            return HttpService.Get<Contact>(Links.contact, cancellationToken);
        }

        public Task<PresenceSubscription> GetPresenceSubscription()
        {
            return GetPresenceSubscription(HttpService.GetNewCancellationToken());
        }

        public async Task<PresenceSubscription> GetPresenceSubscription(CancellationToken cancellationToken)
        {
            return await HttpService.Get<PresenceSubscription>(Links.presenceSubscription, cancellationToken);
        }

        public Task RemoveContactFromPresenceSubscription()
        {
            return RemoveContactFromPresenceSubscription(HttpService.GetNewCancellationToken());
        }

        public async Task RemoveContactFromPresenceSubscription(CancellationToken cancellationToken)
        {
            await HttpService.Delete(Self, cancellationToken);
        }
    }
}
