using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents a set of contacts that have a given privacy relationship with the user. 
    /// The resource gives a view of the contacts that were assigned this privacy relationship as seen in contactPrivacyRelationship. 
    /// </summary>
    public class MyPrivacyRelationship : UCWAModelBase
    {
        [JsonProperty("relationshipLevel")]
        public PrivacyRelationshipLevel RelationshipLevel { get; internal set; }
        
        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("contact")]
            internal UCWAHref[] contact { get; set; }
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
