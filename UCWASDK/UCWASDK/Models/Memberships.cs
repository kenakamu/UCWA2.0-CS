using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// The memberships resource. 
    /// </summary>
    public class Memberships : UCWAModelBaseLink
    {
        [JsonProperty("_embedded")]
        internal InternalEmbedded Embedded { get; set; }

        [JsonIgnore]
        public PresenceSubscriptionMembership PresenceSubscriptionMembership { get { return Embedded.presenceSubscriptionMembership; } }

        internal class InternalEmbedded
        {
            [JsonProperty("presenceSubscriptionMembership")]
            internal PresenceSubscriptionMembership presenceSubscriptionMembership { get; set; }               
        }        

        public async Task<PresenceSubscriptionMemberships> Update(string[] contactUris)
        {
            if (contactUris == null || contactUris.Count() == 0)
                return null;

            JObject body = new JObject();
            body["contactUris"] = new JArray(contactUris);
            return await HttpService.Post<PresenceSubscriptionMemberships>(Self, body);
        }
    }
}
