using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents a persistent, pre-existing group of contacts. 
    /// A distribution group is a mail-enabled Active Directory group object. An application can subscribe to updates from member contacts. Updates include presence, location, or note changes for a specific contact. Currently, distributionGroup is a read-only resource. Unlike other group types, it cannot be managed by any Skype for Business endpoint. An application must call startOrRefreshSubscriptionToContactsAndGroups before it can receive events when a distributionGroup is created, modified, or removed.
    /// </summary>
    public class DistributionGroup : UCWAModelBase
    {
        [JsonProperty("uri")]
        public string Uri { get; internal set; }

        [JsonProperty("id")]
        public string Id { get; internal set; }

        [JsonProperty("name")]
        public string Name { get; internal set; }
        
        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonProperty("_embedded")]
        internal InternalEmbedded Embedded { get; set; }

        [JsonIgnore]
        public Contact[] Contacts { get { return Embedded.contacts; } }

        [JsonIgnore]
        public DistributionGroup[] DistributionGroups { get { return Embedded.distributionGroups; } }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("addToContactList")]
            internal AddToContactList addToContactList { get; set; }
           
            [JsonProperty("expandDistributionGroup")]
            internal ExpandDistributionGroup expandDistributionGroup { get; set; }
     
            [JsonProperty("groupContacts")]
            internal UCWAHref groupContacts { get; set; }
     
            [JsonProperty("removeFromContactList")]
            internal RemoveFromContactList removeFromContactList { get; set; }
     
            [JsonProperty("subscribeToGroupPresence")]
            internal SubscribeToGroupPresence subscribeToGroupPresence { get; set; }
        }

        internal class InternalEmbedded
        {
            [JsonProperty("contact")]
            internal Contact[] contacts { get; set; }

            [JsonProperty("distributionGroup")]
            internal DistributionGroup[] distributionGroups { get; set; }
        }

        public async Task AddToContactList()
        {
            var uri = Links.addToContactList.Href + "?displayName=" + Name + "&smtpAddress=" + Uri;
            await HttpService.Post(Links.addToContactList, "");
        }
        
        public async Task<DistributionGroup> ExpandDistributionGroup()
        {
            return await HttpService.Get<DistributionGroup>(Links.expandDistributionGroup);
        }

        public async Task<GroupContacts> GetGroupContacts()
        {
            return await HttpService.Get<GroupContacts>(Links.groupContacts);
        }

        public async Task RemoveFromContactList()
        {
            await HttpService.Post(Links.removeFromContactList, "");
        }

        public async Task SubscribeToGroupPresence()
        {
            await HttpService.Post(Links.subscribeToGroupPresence, "");
        }
    }
}
