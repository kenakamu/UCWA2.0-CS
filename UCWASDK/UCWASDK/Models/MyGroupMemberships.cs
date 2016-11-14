using Newtonsoft.Json;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// A collection of groupMembership resources, each of which uniquely links a contact to a group. 
    /// This collection is read-only: it can be used only to view existing groupMembership resources. Contacts can be moved into new groups using the Skype for Business client. 
    /// </summary>
    public class MyGroupMemberships : UCWAModelBaseLink
    {
        [JsonProperty("_embedded")]
        internal InternalEmbedded Embedded { get; set; }

        [JsonIgnore]
        public MyGroupMembership[] GroupMemberships { get { return Embedded.groupMemberships; } }

        internal class InternalEmbedded
        {
            [JsonProperty("myGroupMembership")]
            internal MyGroupMembership[] groupMemberships { get; set; }
        }
    }
}
