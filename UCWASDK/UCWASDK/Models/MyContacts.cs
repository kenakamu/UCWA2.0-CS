using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// A collection of contact resources that belong to the logged-on user. 
    /// This collection is read-only: it can be used only to view existing contact resources that are on the logged-on user's contact list. New contacts are added using the Skype for Business client. 
    /// </summary>
    public class MyContacts : UCWAModelBase
    {
        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonProperty("_embedded")]
        internal InternalEmbedded Embedded { get; set; }

        [JsonIgnore]
        public Contact[] Contacts { get { return Embedded.contacts; } }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("contact")]
            internal UCWAHref[] contact { get; set; }
        }

        internal class InternalEmbedded
        {
            [JsonProperty("contact")]
            internal Contact[] contacts { get; set; }
        }

        public Task<List<Contact>> GetContacts()
        {
            return GetContacts(HttpService.GetNewCancellationToken());
        }
        public Task<List<Contact>> GetContacts(CancellationToken cancellationToken)
        {
            return HttpService.GetList<Contact>(Links.contact, cancellationToken);
        }
    }
}
