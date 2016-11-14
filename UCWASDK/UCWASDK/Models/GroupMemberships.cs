using Newtonsoft.Json;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents a collection of all the group memberships for a particular group
    /// </summary>
    public class GroupMemberships : UCWAModelBaseLink
    {
        [JsonProperty("_embedded")]
        internal InternalEmbedded Embedded { get; set; }

        [JsonIgnore]
        public MyGroupMembership2[] MyGroupMemberships { get { return Embedded.myGroupMemberships; } }

        internal class InternalEmbedded
        {
            [JsonProperty("myGroupMembership")]
            internal MyGroupMembership2[] myGroupMemberships { get; set; }               
        }
    }
}
