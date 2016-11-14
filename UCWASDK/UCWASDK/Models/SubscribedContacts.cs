using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// A collection of contacts for which the logged-on user has created active presence subscriptions. 
    /// This collection is read-only: it can be used only to view contacts with existing presence subscriptions. New presence subscriptions are created by using either the presenceSubscriptions resource or the subscribeToGroupPresence resource. These contacts can be members of a group or the result of an ad hoc subscription.
    /// </summary>
    public class SubscribedContacts : UCWAModelBase
    {
        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("batch")]
            internal UCWAHref batch { get; set; }
    
            [JsonProperty("contact")]
            internal UCWAHref[] contact { get; set; }
        }

        public async Task<Batch> GetBatch()
        {
            return await HttpService.Get<Batch>(Links.batch);
        }

        public async Task<List<Contact>> GetContacts()
        {
            return await HttpService.GetList<Contact>(Links.contact);
        }
    }
}
