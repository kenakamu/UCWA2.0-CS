using Newtonsoft.Json;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// A collection of groups in the contact list of the logged-on user. 
    /// This collection is read-only: it can be used only to view existing group resources. New groups can be created using the Skype for Business desktop client. 
    /// </summary>
    public class MyGroups : UCWAModelBaseLink
    {
        [JsonProperty("_embedded")]
        internal InternalEmbedded Embedded { get; set; }

        [JsonIgnore]
        public Group[] Groups { get { return Embedded.groups; } }

        [JsonIgnore]
        public DefaultGroup DefaultGroup { get { return Embedded.defaultGroup; } }

        [JsonIgnore]
        public PinnedGroup PinnedGroup { get { return Embedded.pinnedGroup; } }

        [JsonIgnore]
        public DistributionGroup[] DistributionGroups { get { return Embedded.distributionGroups; } }

        internal class InternalEmbedded
        {
            [JsonProperty("group")]
            internal Group[] groups { get; set; }

            [JsonProperty("defaultGroup")]
            internal DefaultGroup defaultGroup { get; set; }

            [JsonProperty("pinnedGroup")]
            internal PinnedGroup pinnedGroup { get; set; }

            [JsonProperty("distributionGroup")]
            internal DistributionGroup[] distributionGroups { get; set; }
        }
    }    
}
