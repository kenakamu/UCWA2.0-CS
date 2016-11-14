using Newtonsoft.Json;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// A collection of presenceSubscriptionMembership resources.
    /// </summary>
    public class PresenceSubscriptionMemberships : UCWAModelBaseLink
    {
        [JsonProperty("_embedded")]
        internal InternalEmbedded Embedded { get; set; }

        [JsonIgnore]
        public PresenceSubscriptionMembership[] Memberships { get { return Embedded.memberships; } }
        internal class InternalEmbedded
        {
            [JsonProperty("presenceSubscriptionMembership")]
            internal PresenceSubscriptionMembership[] memberships { get; set; }
        }
    }
}
