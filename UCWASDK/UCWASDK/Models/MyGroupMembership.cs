using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading;
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

        public Task<Contact> GetContact()
        {
            return GetContact(HttpService.GetNewCancellationToken());
        }
        public Task<Contact> GetContact(CancellationToken cancellationToken)
        {
            return HttpService.Get<Contact>(Links.contact, cancellationToken);
        }

        public Task<DefaultGroup> GetDefaultGroup()
        {
            return GetDefaultGroup(HttpService.GetNewCancellationToken());
        }
        public Task<DefaultGroup> GetDefaultGroup(CancellationToken cancellationToken)
        {
            return HttpService.Get<DefaultGroup>(Links.defaultGroup, cancellationToken);
        }

        public Task<Group> GetGroup()
        {
            return GetGroup(HttpService.GetNewCancellationToken());
        }
        public Task<Group> GetGroup(CancellationToken cancellationToken)
        {
            return HttpService.Get<Group>(Links.group, cancellationToken);
        }

        public Task<PinnedGroup> GetPinnedGroup()
        {
            return GetPinnedGroup(HttpService.GetNewCancellationToken());
        }
        public Task<PinnedGroup> GetPinnedGroup(CancellationToken cancellationToken)
        {
            return HttpService.Get<PinnedGroup>(Links.pinnedGroup, cancellationToken);
        }
    }
}
