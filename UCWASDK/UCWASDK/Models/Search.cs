using Newtonsoft.Json;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Provides a way to search for contacts. 
    /// This resource is used to search through the address book of the user's organization. The fields searched include name, email, URI, and phone number. 
    /// </summary>
    public class Search : UCWAModelBaseLink
    {
        [JsonProperty("moreResultsAvailable")]
        public bool MoreResultsAvailable { get; internal set; }

        [JsonProperty("_embedded")]
        internal InternalEmbedded Embedded { get; set; }

        [JsonIgnore]
        public Contact[] Contacts { get { return Embedded.contacts; } }

        [JsonIgnore]
        public DistributionGroup[] DistributionGroups { get { return Embedded.distributionGroups; } }

        internal class InternalEmbedded
        {
            [JsonProperty("contact")]
            internal Contact[] contacts { get; set; }

            [JsonProperty("distributionGroup")]
            internal DistributionGroup[] distributionGroups { get; set; }
        }
    }
}
