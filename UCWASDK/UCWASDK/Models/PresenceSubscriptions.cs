using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the user's set of presenceSubscription resources.
    /// This resource can be used to create new presenceSubscriptions as well as to modify and delete existing ones. 
    /// </summary>
    public class PresenceSubscriptions : UCWAModelBaseLink
    {
        [JsonProperty("_embedded")]
        internal InternalEmbedded Embedded { get; set; }

        [JsonIgnore]
        public PresenceSubscription[] Subscriptions { get { return Embedded.subscriptions; } }

        internal class InternalEmbedded
        {
            [JsonProperty("presenceSubscription")]
            internal PresenceSubscription[] subscriptions { get; set; }               
        }

        public async Task<PresenceSubscription> SubscribeToContactsPresence(string[] uris, int duration = 30)
        {
            return await SubscribeToContactsPresence(HttpService.GetNewCancellationToken(), uris, duration);
        }

        public async Task<PresenceSubscription> SubscribeToContactsPresence(CancellationToken cancellationToken, string[] uris, int duration = 30)
        {
            if (uris == null || uris.Count() == 0)
                return null;

            if (duration < 10 || duration > 30)
                return null;
            
            JObject body = new JObject();
            body["duration"] = duration;
            body["uris"] = new JArray(uris);
            return await HttpService.Post<PresenceSubscription>(Self, body, cancellationToken);
        }

        public async Task<PresenceSubscription> Renew()
        {
            return await Renew(HttpService.GetNewCancellationToken());
        }

        public async Task<PresenceSubscription> Renew(CancellationToken cancellationToken)
        {
            var uri = Self + "?duration=30";
            return await HttpService.Post<PresenceSubscription>(uri, null, cancellationToken);
        }
    }
}
