using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents a user's persistent, personal group.
    /// An application can subscribe to updates from members of this group. Updates include presence, location, or note changes for a specific contact. Currently, group is a read-only resource and can be managed by other endpoints. An application must call startOrRefreshSubscriptionToContactsAndGroups before it can receive events when a group is created, modified, or removed.
    /// </summary>
    public class Group : UCWAModelBase
    {
        [JsonProperty("id")]
        public string Id { get; internal set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("expandDistributionGroup")]
            internal ExpandDistributionGroup expandDistributionGroup { get; set; }
 
            [JsonProperty("groupContacts")]
            internal UCWAHref groupContacts { get; set; }
 
            [JsonProperty("groupMemberships")]
            internal UCWAHref groupMemberships { get; set; }
 
            [JsonProperty("subscribeToGroupPresence")]
            internal SubscribeToGroupPresence subscribeToGroupPresence { get; set; }
        }

        public Task<DistributionGroup> ExpandDistributionGroup()
        {
            return ExpandDistributionGroup(HttpService.GetNewCancellationToken());
        }
        public async Task<DistributionGroup> ExpandDistributionGroup(CancellationToken cancellationToken)
        {
            return await HttpService.Get<DistributionGroup>(Links.expandDistributionGroup, cancellationToken);
        }

        public Task<GroupContacts> GetGroupContacts()
        {
            return GetGroupContacts(HttpService.GetNewCancellationToken());
        }
        public async Task<GroupContacts> GetGroupContacts(CancellationToken cancellationToken)
        {
            return await HttpService.Get<GroupContacts>(Links.groupContacts, cancellationToken);
        }

        public Task<GroupMemberships> GetGroupMemberships()
        {
            return GetGroupMemberships(HttpService.GetNewCancellationToken());
        }
        public async Task<GroupMemberships> GetGroupMemberships(CancellationToken cancellationToken)
        {
            return await HttpService.Get<GroupMemberships>(Links.groupMemberships, cancellationToken);
        }

        public Task<PresenceSubscription> SubscribeToGroupPresence(int duration = 30)
        {
            return SubscribeToGroupPresence(HttpService.GetNewCancellationToken());
        }
        public async Task<PresenceSubscription> SubscribeToGroupPresence(CancellationToken cancellationToken, int duration = 30)
        {
            if (duration > 30 && duration < 10)
                return null;

            var uri = Links.subscribeToGroupPresence.Href + "&duration=" + duration;
            return await HttpService.Post<PresenceSubscription>(uri, null, cancellationToken);
        }
    }    
}
