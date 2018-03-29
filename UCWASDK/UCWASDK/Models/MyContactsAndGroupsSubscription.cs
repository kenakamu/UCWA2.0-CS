using Microsoft.Skype.UCWA.Services;
using Microsoft.Skype.UCWA.Enums;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the subscription to a user's contacts and groups. 
    /// The application can use this resource to keep track of a user's contacts and groups via the event channel. Updates include the addition, removal, or modification of groups or contacts. Additionally, an update on the event channel will inform the application that the subscription is about to expire. The application can then choose to refresh the subscription. Note that, unlike presenceSubscription, this resource does not subscribe to presence, note, or location. 
    /// </summary>
    public class MyContactsAndGroupsSubscription : UCWAModelBase
    {
        [JsonProperty("state")]
        public SubscriptionState State { get; internal set; }

        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("startOrRefreshSubscriptionToContactsAndGroups")]
            internal StartOrRefreshSubscriptionToContactsAndGroups startOrRefreshSubscriptionToContactsAndGroups { get; set; }

            [JsonProperty("stopSubscriptionToContactsAndGroups")]
            internal StopSubscriptionToContactsAndGroups stopSubscriptionToContactsAndGroups { get; set; }
        }

        public Task StartOrRefreshSubscriptionToContactsAndGroups(int duration)
        {
            return StartOrRefreshSubscriptionToContactsAndGroups(duration, HttpService.GetNewCancellationToken());
        }
        public Task StartOrRefreshSubscriptionToContactsAndGroups(int duration, CancellationToken cancellationToken)
        {
            if (duration > 60 && duration < 10)
                return Task.FromResult<object>(null);

            var uri = Links.startOrRefreshSubscriptionToContactsAndGroups.Href + "?duration=" + duration;
            return HttpService.Post(uri, "", cancellationToken);
        }

        public Task StopSubscriptionToContactsAndGroups()
        {
            return StopSubscriptionToContactsAndGroups(HttpService.GetNewCancellationToken());
        }
        public Task StopSubscriptionToContactsAndGroups(CancellationToken cancellationToken)
        {
            return HttpService.Post(Links.stopSubscriptionToContactsAndGroups, "", cancellationToken);
        }
    }
}
