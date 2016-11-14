using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// A hub for the people with whom the logged-on user can communicate, using Skype for Business. 
    /// This resource provides access to resources that enable the logged-on user to find, organize, communicate with, and subscribe to the presence of other people. 
    /// </summary>
    public class People : UCWAModelBase
    {
        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("myContactsAndGroupsSubscription")]
            internal UCWAHref myContactsAndGroupsSubscription { get; set; }
        
            [JsonProperty("myContacts")]
            internal UCWAHref myContacts { get; set; }
        
            [JsonProperty("myGroupMemberships")]
            internal UCWAHref myGroupMemberships { get; set; }
        
            [JsonProperty("myGroups")]
            internal UCWAHref myGroups { get; set; }

            [JsonProperty("myPrivacyRelationships")]
            internal UCWAHref myPrivacyRelationships { get; set; }
        
            [JsonProperty("presenceSubscriptionMemberships")]
            internal UCWAHref presenceSubscriptionMemberships { get; set; }
        
            [JsonProperty("presenceSubscriptions")]
            internal UCWAHref presenceSubscriptions { get; set; }
        
            [JsonProperty("search")]
            internal UCWAHref search { get; set; }
        
            [JsonProperty("subscribedContacts")]
            internal UCWAHref subscribedContacts { get; set; }
        }

        public async Task<MyContactsAndGroupsSubscription> GetMyContactsAndGroupsSubscription()
        {
            return await HttpService.Get<MyContactsAndGroupsSubscription>(Links.myContactsAndGroupsSubscription);
        }

        public async Task<MyContacts> GetMyContacts()
        {
            return await HttpService.Get<MyContacts>(Links.myContacts);
        }

        public async Task<MyGroupMemberships2> GetMyGroupMemberships()
        {
            return await HttpService.Get<MyGroupMemberships2>(Links.myGroupMemberships);
        }

        public async Task<MyGroups2> GetMyGroups()
        {
            return await HttpService.Get<MyGroups2>(Links.myGroups);
        }

        public async Task<MyPrivacyRelationships> GetMyPrivacyRelationships()
        {
            return await HttpService.Get<MyPrivacyRelationships>(Links.myPrivacyRelationships);
        }

        public async Task<PresenceSubscriptionMemberships> GetPresenceSubscriptionMemberships()
        {
            return await HttpService.Get<PresenceSubscriptionMemberships>(Links.presenceSubscriptionMemberships);
        }

        public async Task<PresenceSubscriptions> GetPresenceSubscriptions()
        {
            return await HttpService.Get<PresenceSubscriptions>(Links.presenceSubscriptions);
        }

        public async Task<Search2> Search(string query)
        {
            if (string.IsNullOrEmpty(query))
                return null;

            var uri = Links.search.Href + "?query=" + query;
            return await HttpService.Get<Search2>(uri);
        }

        public async Task<SubscribedContacts> GetSubscribedContacts()
        {
            return await HttpService.Get<SubscribedContacts>(Links.subscribedContacts);
        }        
    }
}
