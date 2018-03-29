using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
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

        public Task<MyContactsAndGroupsSubscription> GetMyContactsAndGroupsSubscription()
        {
            return GetMyContactsAndGroupsSubscription(HttpService.GetNewCancellationToken());
        }

        public Task<MyContactsAndGroupsSubscription> GetMyContactsAndGroupsSubscription(CancellationToken cancellationToken)
        {
            return HttpService.Get<MyContactsAndGroupsSubscription>(Links.myContactsAndGroupsSubscription, cancellationToken);
        }

        public Task<MyContacts> GetMyContacts()
        {
            return GetMyContacts(HttpService.GetNewCancellationToken());
        }

        public Task<MyContacts> GetMyContacts(CancellationToken cancellationToken)
        {
            return HttpService.Get<MyContacts>(Links.myContacts, cancellationToken);
        }

        public Task<MyGroupMemberships2> GetMyGroupMemberships()
        {
            return GetMyGroupMemberships(HttpService.GetNewCancellationToken());
        }

        public Task<MyGroupMemberships2> GetMyGroupMemberships(CancellationToken cancellationToken)
        {
            return HttpService.Get<MyGroupMemberships2>(Links.myGroupMemberships, cancellationToken);
        }

        public Task<MyGroups2> GetMyGroups()
        {
            return GetMyGroups(HttpService.GetNewCancellationToken());
        }

        public Task<MyGroups2> GetMyGroups(CancellationToken cancellationToken)
        {
            return HttpService.Get<MyGroups2>(Links.myGroups, cancellationToken);
        }

        public Task<MyPrivacyRelationships> GetMyPrivacyRelationships()
        {
            return GetMyPrivacyRelationships(HttpService.GetNewCancellationToken());
        }

        public Task<MyPrivacyRelationships> GetMyPrivacyRelationships(CancellationToken cancellationToken)
        {
            return HttpService.Get<MyPrivacyRelationships>(Links.myPrivacyRelationships, cancellationToken);
        }

        public Task<PresenceSubscriptionMemberships> GetPresenceSubscriptionMemberships()
        {
            return GetPresenceSubscriptionMemberships(HttpService.GetNewCancellationToken());
        }

        public Task<PresenceSubscriptionMemberships> GetPresenceSubscriptionMemberships(CancellationToken cancellationToken)
        {
            return HttpService.Get<PresenceSubscriptionMemberships>(Links.presenceSubscriptionMemberships, cancellationToken);
        }

        public Task<PresenceSubscriptions> GetPresenceSubscriptions()
        {
            return GetPresenceSubscriptions(HttpService.GetNewCancellationToken());
        }

        public Task<PresenceSubscriptions> GetPresenceSubscriptions(CancellationToken cancellationToken)
        {
            return HttpService.Get<PresenceSubscriptions>(Links.presenceSubscriptions, cancellationToken);
        }

        public Task<Search2> Search(string query)
        {
            return Search(query, HttpService.GetNewCancellationToken());
        }

        public async Task<Search2> Search(string query, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(query))
                return null;

            var uri = Links.search.Href + "?query=" + query;
            return await HttpService.Get<Search2>(uri, cancellationToken);
        }

        public Task<SubscribedContacts> GetSubscribedContacts()
        {
            return GetSubscribedContacts(Links.subscribedContacts, HttpService.GetNewCancellationToken());
        }

        public Task<SubscribedContacts> GetSubscribedContacts(UCWAHref subscribedContacts, CancellationToken cancellationToken)
        {
            return HttpService.Get<SubscribedContacts>(subscribedContacts, cancellationToken);
        }
    }
}
