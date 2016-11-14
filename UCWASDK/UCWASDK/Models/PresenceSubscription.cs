using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the subscription to a user-defined set of contacts. 
    /// This resource allows the application to keep track of members of the subscription. Updates include presence, location, or note changes for a specific contact. Additionally, an update on the event channel will inform the application that the subscription is about to expire. The application can then elect to refresh the subscription. Unlike group, presenceSubscription is not persistent and is typically relevant only for a short duration. 
    /// </summary>
    public class PresenceSubscription : UCWAModelBase
    {
        [JsonProperty("id")]
        public string Id { get; internal set; }

        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("addToPresenceSubscription")]
            internal AddToPresenceSubscription addToPresenceSubscription { get; set; }

            [JsonProperty("memberships")]
            internal UCWAHref memberships { get; set; }
        }

        public async Task<PresenceSubscriptionMemberships> AddToPresenceSubscription(params string[] sips)
        {
            if (sips == null || sips.Length == 0)
                return null;
                        
            JObject body = new JObject();
            JArray contactUris = new JArray();
            foreach (var sip in sips) { contactUris.Add(sip); }
            body["ContactUris"] = contactUris;

            return await HttpService.Post<PresenceSubscriptionMemberships>(Links.addToPresenceSubscription, body);
        }

        public async Task<Memberships> GetMemberships()
        {
            return await HttpService.Get<Memberships>(Links.memberships);
        }

        public async Task Delete()
        {
            await HttpService.Delete(Self);
        }
    }    
}
