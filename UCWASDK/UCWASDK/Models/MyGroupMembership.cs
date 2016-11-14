using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the group membership of a single contact. 
    /// This resource captures a unique pair of contact and group. If a contact appears in multiple groups, there will be a separate resource for each membership of this contact. 
    /// </summary>
    public class MyGroupMembership : UCWAModelBase
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
   
            [JsonProperty("defaultGroup")]
            internal UCWAHref defaultGroup { get; set; }
   
            [JsonProperty("group")]
            internal UCWAHref group { get; set; }
   
            [JsonProperty("pinnedGroup")]
            internal UCWAHref pinnedGroup { get; set; }
        }

        public async Task<Contact> GetContact()
        {
            return await HttpService.Get<Contact>(Links.contact);
        }

        public async Task<DefaultGroup> GetDefaultGroup()
        {
            return await HttpService.Get<DefaultGroup>(Links.defaultGroup);
        }

        public async Task<Group> GetGroup()
        {
            return await HttpService.Get<Group>(Links.group);
        }

        public async Task<PinnedGroup> GetPinnedGroup()
        {
            return await HttpService.Get<PinnedGroup>(Links.pinnedGroup);
        }
    }
}
